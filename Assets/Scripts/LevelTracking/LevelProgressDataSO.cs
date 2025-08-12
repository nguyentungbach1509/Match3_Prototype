using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelTracking
{
    [CreateAssetMenu(fileName = "LevelProgressData", menuName = "Game/Level Progress Data")]
    public class LevelProgressDataSO : ScriptableObject
    {
        public LevelProgressData Data;
    }

    [System.Serializable]
    public class LevelProgressData
    {
        public int totalLevels;
        public int currentLevel;
        public List<bool> claimedRewards;
    }
}


