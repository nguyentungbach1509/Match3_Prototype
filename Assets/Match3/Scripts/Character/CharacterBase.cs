using UnityEngine;

namespace Match3.Scripts.Character
{
    public class CharacterBase : MonoBehaviour
    {
        [SerializeField] protected StatsData data;
        [SerializeField] protected CharacterCanvas canvas;

        protected CharacterStats stats;

        public virtual void Init()
        {
            stats = new CharacterStats(data, canvas);
        }
    }
}

