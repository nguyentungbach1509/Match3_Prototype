using SubScript.Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SubScript.VFXEffect
{
    public class VFX : PoolableComponent
    {
        [SerializeField] AnimationClip clip;

        public Action OnVFXEnd;

        public override void OnSpawn()
        {
            base.OnSpawn();
        }

        public override void OnDespawn()
        {
            OnVFXEnd?.Invoke();
            base.OnDespawn(); 
        }

        public float EndOffsetTime => clip.length+.125f;
        public float EndTime => clip.length;
    }
}

