using UnityEngine;

namespace Player
{
    public class HurtableDino : MonoBehaviour, IHurtable
    {
        [SerializeField]
        private Collider m_HurtBox;
        [SerializeField]
        private LayerMask m_hazardMask;  
        public Collider HurtBox => m_HurtBox;

        public LayerMask HazardMask => m_hazardMask;


        public void Update()
        {
            // Check if hurtbox overlap with a hitbox that lives on HazardMask layer
            Collider[] hits = Physics.OverlapBox(
                m_HurtBox.bounds.center,
                m_HurtBox.bounds.extents,
                m_HurtBox.transform.rotation,
                m_hazardMask
            );

            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    if (hit != m_HurtBox) // ignore self
                    {
                        Hurt(hit.GetComponent<IHazard>());
                        break;
                    }
                }
            }
        }

        // What should happen when dino is hurt?
        // Stop carrying.
        // Cancel Movement.
        // Invinsibility frames.
        // Change face.
        // Play sound
        public void Hurt(IHazard hazard)
        {
            gameObject.GetComponent<PlayerMovement>().Hurt();
            // Cancel movement for a bit
        }
    }
}
