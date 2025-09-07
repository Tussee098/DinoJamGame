using UnityEngine;

namespace Hazards
{
    public class Hazard : MonoBehaviour, IHazard
    {
        [SerializeField]
        private int damage;
        public int Damage => damage;



    }
}
