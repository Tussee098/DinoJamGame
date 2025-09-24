using System;
using UnityEngine;

namespace Player {
    public class PlayerMovement : MonoBehaviour
    {
        public Rigidbody rb;
        [Header("Platforming")]
        public float RunningSpeed;

        [Header("Swimming")]
        private bool m_swimming = true;
        public float SwimmingSpeed;

        [Header("Jumping")]
        public float jumpForce = 7f;
        public LayerMask groundMask;
        public Transform groundCheck;
        public float groundRadius = 0.2f;

        public float coyoteTime = 0.1f, jumpBufferTime = 0.1f;
        private float coyoteTimer, jumpBufferTimer;
        private bool m_isFlutterJumping;
        private bool m_inAir;

        [Header("Hurting variables")]
        private bool hurting;
        private bool movementImpaired;
        public float iFramesTime;
        public float movementLossTime;

        private float m_iFramesTimer;
        private float m_movementLossTimer;

        [Header("Fluttering")]
        public float maxFallSpeed = -3.0f;
        public float flutterHz = 10.0f;
        public float flutterKick = 10.0f;
        public float flutterTime;
        public int maxflutterCharges;

        private float flutterTimer = 0f;
        private int m_flutterCharges;

        private bool m_walking;

        private DinoAnimationBridge m_anim;

        public AudioSource JumpSound;
        public AudioSource PropellerSound;

        void Start()
        {
            m_flutterCharges = maxflutterCharges;
            m_anim = GetComponentInChildren<DinoAnimationBridge>(true);
        }
        // Update is called once per frame
        void Update()
        {
            float dt = Time.deltaTime;
            if (hurting)
            {
                UpdateTimers(dt);
                if (m_iFramesTimer <= 0)
                {
                    RevertHurting();
                }
                if (m_movementLossTimer <= 0)
                {
                    movementImpaired = false;
                }
            }
            if (movementImpaired) return;
            StandardControl(dt);
            if (m_swimming == true)
            {
                SwimmingControl(dt);
            } else
            {
                JumpingControl();
            }

            if(m_isFlutterJumping)
            {
                FlutterJump();
            }
            if (Input.GetButtonDown("Pickup"))
            {
                GetComponent<CarryDino>().TryToPickup();
            }

        }

        private void RevertHurting()
        {
            hurting = false;
            // Activate Hurting
            gameObject.GetComponent<HurtableDino>().enabled = true;
            // Change face
            gameObject.GetComponentInChildren<DinoFaceBridge>().SetFace(DinoFaceBridge.FaceEnum.Hurt);
        }

        private void UpdateTimers(float dt)
        {
            m_movementLossTimer -= dt;
            m_iFramesTimer -= dt;
        }

        private void StandardControl(float dt)
        {
            var tmpSpeed = RunningSpeed;
            if (m_swimming)
            {
                tmpSpeed = SwimmingSpeed;
            }
            float h = Input.GetAxis("Horizontal") * tmpSpeed;

            if (h != 0f && m_swimming)
            {
                m_anim.PlaySwimSide(true);
            }
            else
            {
                PropellerSound.Play();
                m_anim.PlaySwimSide(false);
            }
            rb.linearVelocity = new Vector3(h, rb.linearVelocity.y, rb.linearVelocity.z);
        }

        private void SwimmingControl(float dt)
        {
            float v = Input.GetAxis("Vertical") * SwimmingSpeed;
            
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, v, rb.linearVelocity.z);
        }
        private void JumpingControl()
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpBufferTimer = jumpBufferTime;
            }
            bool grounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);
            
            if (grounded) // Reset coyoteTimeer
            {
                if (!m_walking) m_anim.StartLand();
                m_walking = true;
                coyoteTimer = coyoteTime;
                
                ResetFlutter();
            } else
            {
                coyoteTimer -= Time.deltaTime; //Update coyote timer
                m_walking = false;
            }
            

            if (jumpBufferTimer > 0f) jumpBufferTimer -= Time.deltaTime; // Update buffertimer

            if (!grounded && !m_isFlutterJumping && jumpBufferTimer > 0f && rb.linearVelocity.y < 0f && m_flutterCharges > 0)
            {
                m_anim.PlayFlutter();
                m_isFlutterJumping = true;
                m_flutterCharges -= 1;
                flutterTimer = flutterTime;
            }

            else if (jumpBufferTimer > 0f && coyoteTimer > 0f) // Check if normal jump or flutterJump
            {
                Vector3 v = rb.linearVelocity; v.y = 0f; rb.linearVelocity = v;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpBufferTimer = 0f; coyoteTimer = 0f;
                JumpSound.Play();
                m_anim.PlayJump();
            }
        }

        private void ResetFlutter()
        {
            m_isFlutterJumping = false;
            m_flutterCharges = maxflutterCharges;
        }

        private void FlutterJump()
        {
            var v = rb.linearVelocity;
            if (v.y < maxFallSpeed) v.y = maxFallSpeed;
            rb.linearVelocity = v;

            float pulse = Mathf.Sin(Time.deltaTime * 2f * Mathf.PI);
            if (pulse > 0f)
            {
                rb.AddForce(Vector3.up * (flutterKick * pulse), ForceMode.VelocityChange);
            }

            flutterTimer -= Time.deltaTime;
            if (flutterTimer <= 0f) m_isFlutterJumping = false;
        }

        public void StartJumpPhase()
        {
            m_swimming = false;
            rb.useGravity = true;
        }

        public void StartSwimPhase()
        {
            m_anim.StartSwim();
            m_swimming = true;
            rb.useGravity = false;
        }

        public void Hurt()
        {
            ResetIframesTimer();
            hurting = true;
            movementImpaired = true;
            // Disable HurtBox
            gameObject.GetComponent<HurtableDino>().enabled = false;
            //Change face
            gameObject.GetComponentInChildren<DinoFaceBridge>().SetFace(DinoFaceBridge.FaceEnum.Hurt);

            m_anim.PlayHit();
            if(m_swimming)
            {
                m_anim.StartSwim();
            } else
            {
                m_anim.StartWalk();
            }
        }

        private void ResetIframesTimer()
        {
            m_iFramesTimer = iFramesTime;
            m_movementLossTimer = movementLossTime;
        }
        public void RespawnReset()
        {
            m_iFramesTimer = 0;
            m_movementLossTimer = 0;
            hurting = false;
            movementImpaired = false;
        }
    }
}
