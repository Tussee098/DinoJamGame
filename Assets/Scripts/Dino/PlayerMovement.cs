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

        [Header("Hurting variables")]
        private bool hurting;
        private bool movementImpaired;
        public float iFramesTime;
        public float movementLossTime;

        private float m_iFramesTimer;
        private float m_movementLossTimer;

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
            if (Input.GetButtonDown("Pickup"))
            {
                GetComponent<CarryDino>().TryToPickup();
            }

        }

        private void RevertHurting()
        {
            hurting = false;
            gameObject.GetComponent<HurtableDino>().enabled = true;
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

            rb.linearVelocity = new Vector3(h * dt, rb.linearVelocity.y, rb.linearVelocity.z);
        }

        private void SwimmingControl(float dt)
        {
            float v = Input.GetAxis("Vertical") * SwimmingSpeed;

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, v * dt, rb.linearVelocity.z);
        }
        private void JumpingControl()
        {
            if (Input.GetButtonDown("Jump")) jumpBufferTimer = jumpBufferTime;

            bool grounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);
            if (grounded) coyoteTimer = coyoteTime; else coyoteTimer -= Time.deltaTime;
            if (jumpBufferTimer > 0f) jumpBufferTimer -= Time.deltaTime;

            if (jumpBufferTimer > 0f && coyoteTimer > 0f)
            {
                Vector3 v = rb.linearVelocity; v.y = 0f; rb.linearVelocity = v;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpBufferTimer = 0f; coyoteTimer = 0f;
            }
        }

        public void StartJumpPhase()
        {
            m_swimming = false;
            rb.useGravity = true;
        }

        public void StartSwimPhase()
        {
            m_swimming = true;
            rb.useGravity = false;
        }

        public void Hurt()
        {
            ResetIframesTimer();
            hurting = true;
            movementImpaired = true;
            gameObject.GetComponent<HurtableDino>().enabled = false;
            // Disable HurtBox
            // Todo - Change face

        }

        private void ResetIframesTimer()
        {
            m_iFramesTimer = iFramesTime;
            m_movementLossTimer = movementLossTime;
        }
    }
}
