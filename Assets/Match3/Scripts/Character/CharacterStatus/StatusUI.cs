using SubScript.Pooling;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Scripts.Character
{
    public class StatusUI : MonoBehaviour, IPoolable
    {
        [SerializeField] Image fill;

        public void OnDespawn()
        {
            throw new System.NotImplementedException();
        }

        public void OnSpawn()
        {
            throw new System.NotImplementedException();
        }

        public void Apply(Status status)
        {
            IEnumerator ApplyUpdate()
            {

            }
        }
    }
}

