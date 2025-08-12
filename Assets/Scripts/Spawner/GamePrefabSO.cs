using System.Collections.Generic;
using UnityEngine;
using SubScript.VFXEffect;


namespace SubScript.GamePrefabController.Data
{
    [CreateAssetMenu(fileName = "GamePrefabs", menuName = "Game/Prefabs")]
    public class GamePrefabSO : ScriptableObject
    {
        [Header("Vfx Prefab")]
        public List<VFXData> VfxPrefabs = new List<VFXData>();

    }

}


