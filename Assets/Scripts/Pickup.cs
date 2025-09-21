using UnityEngine;

namespace Assets.Scripts
{
    public enum PickupType
    {
        RepairBoard = 0,
        OxygenTank = 1,
    }

    public class Pickup : MonoBehaviour, IPickupable
    {
        [SerializeField]
        public PickupType type;
        public bool TryPickup(ICarrier instigator)
        {
            instigator.pickupType = type;
            Object.Destroy(gameObject);
            return true;
        }
    }
}
