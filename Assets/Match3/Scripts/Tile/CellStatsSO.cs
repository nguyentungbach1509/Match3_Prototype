using Match3.Scripts.Character;
using Match3.Subscripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Scripts
{
    [CreateAssetMenu(fileName = "CellStats", menuName = "Data/Cell/Stats")]
    public class CellStatsSO : ScriptableObject
    {
        [SerializeField] List<CellStats> stats;
        private Dictionary<ECellType, CellStats> dictStats = new();

        public CellStats GetStats(ECellType type)
        {
            if(dictStats.TryGetValue(type, out var value)) return value;
            CellStats cellStats = stats.Find(e => e.Type == type);
            dictStats.Add(type, cellStats);
            return cellStats;
        }
    }

    [Serializable]
    public class CellStats
    {
        public ECellType Type;
    }
}

