using Match3.Scripts.Data;
using Match3.Subscripts;
using SubScript.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Scripts.Character
{
    public class StatusController : MonoBehaviour
    {
        [SerializeField] StatusUI uiPrefab;

        private DataManager data => DataManager.Instance;

        private ObjectPool<StatusUI> pool;
        private CharacterStats stats;

        private Dictionary<ECellType, StatusUI> cachedStatusUI;
        private Dictionary<ECellType, StatusEffect> cachedStatus;

        public void Init(CharacterStats stats)
        {
            cachedStatus = new Dictionary<ECellType, StatusEffect>();
            cachedStatusUI = new Dictionary<ECellType, StatusUI>();
            this.stats = stats;
            pool = PoolManager.CreateOrGetPool(uiPrefab);
        }

        public void Apply(ECellType status, int multi=1)
        {
            if(cachedStatus.ContainsKey(status) && cachedStatusUI.ContainsKey(status))
            {
                StatusEffect statusEff = cachedStatus[status];
                if(multi > 1)
                cachedStatusUI[status].Apply(statusEff, this, stats, multi);
                return;
            }  
            StatusEffect statusEffect = data.GetStatus(status).CreateEffect(stats);
            cachedStatus[status] = statusEffect;
            if (statusEffect.ApplyOnUI)
            {
                StatusUI statusUI = pool.Spawn(Vector3.zero, Quaternion.identity);
                statusUI.transform.SetParent(transform, false);
                statusUI.Apply(statusEffect, this, stats, multi);
                cachedStatusUI[status] = statusUI;
                return;
            }
            statusEffect.OnApply(null, null);
        }

        public void RemoveStatus(ECellType key, StatusUI statusUI)
        {
            cachedStatusUI.Remove(key);
            cachedStatus.Remove(key);
            pool.Despawn(statusUI);
        }

        public bool GotStatus(ECellType key) => cachedStatus.ContainsKey(key);
        public bool GotFrozen() => cachedStatus.ContainsKey(ECellType.Ice);
        public StatusEffect GetStatus(ECellType key) => cachedStatus[key];
    }
}

