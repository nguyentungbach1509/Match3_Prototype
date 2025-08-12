using UnityEngine;

namespace Test.Script
{
    public class MoveSpeedAuthoring : MonoBehaviour
    {
        [SerializeField] private float speed;
        public float Speed => speed;    
    }
}