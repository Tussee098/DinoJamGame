using UnityEngine;

namespace Assets.Scripts
{
    public enum PickupType
    {
        None = 0,
        RepairBoard = 1,
        OxygenTank = 2,
    }

    public class Pickup : MonoBehaviour, IPickupable
    {
        [SerializeField]
        private PickupType m_type;

        public PickupType Type => m_type;
        public bool TryPickup(ICarrier instigator)
        {

            gameObject.SetActive(false);
            return true;
        }
        public void Respawn()
        {
            gameObject.SetActive(true);
        }
    }
}
