using UnityEngine;

namespace Assets.Scripts
{
    public class Pickup : MonoBehaviour, IPickupable
    {
        [SerializeField] private int weight;
        public int Weight { get => weight; set => weight = value; }

        public bool TryPickup(ICarrier instigator)
        {
            Object.Destroy(gameObject);
            return true;
        }
    }
}
