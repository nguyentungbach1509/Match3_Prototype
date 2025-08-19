using SubScript.Pooling;
using System.Collections;
using UnityEngine;

namespace Match3.Scripts.Character
{
    public class StatusController : MonoBehaviour
    {
        [SerializeField] StatusUI statusUI;

        private ObjectPool<StatusUI> pool;
        private CharacterStats stats;

        public void Init(CharacterStats characterStats)
        {
            stats = characterStats;
            pool = PoolManager.CreateOrGetPool(statusUI);
        }

        private void ApplyEffect(Status status)
        {
            IEnumerator Apply()
            {

            }
        }

        
    }
}

