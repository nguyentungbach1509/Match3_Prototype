using UnityEngine;

namespace Match3.Scripts.Spawner
{
    [CreateAssetMenu(fileName = "Spawn Settings", menuName = "Spawner/Data/Spawn Settings")]
    public class SpawnSettings : ScriptableObject
    {
        [SerializeField] AnimationCurve tier1Chance;
        [SerializeField] AnimationCurve tier2Chance;
        [SerializeField] AnimationCurve tier3Chance;

        public float GetTierChance(EEnemyTier tier, float percent)
        {
            switch (tier)
            {
                case EEnemyTier.Tier1: return tier1Chance.Evaluate(percent);
                case EEnemyTier.Tier2: return tier2Chance.Evaluate(percent);
                case EEnemyTier.Tier3: return tier3Chance.Evaluate(percent);
                default: return 0;
            }
        }
    }
}





