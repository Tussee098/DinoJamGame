using System;
using UnityEngine;

namespace Hazards {
    public enum DirectionEnum
    {
        Left, Right
    }
    public class Fish : MonoBehaviour
    {
        
        public Fish(DirectionEnum direction)
        {
            m_direction = direction;
        }

        public float speed;
        private DirectionEnum m_direction;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }
}