using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubScript.Pooling
{
    public interface IPoolable
    {
        void OnSpawn();
        void OnDespawn();
    }
}

