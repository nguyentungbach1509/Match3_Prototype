using UnityEngine;

namespace SubScript.VFXEffect
{
    [CreateAssetMenu(fileName = "VFX Data", menuName = "VFX/Data")]
    public class VFXData : ScriptableObject
    {
        [SerializeField] VFX vfxPrefab;
        [SerializeField] string key;
        public VFX Prefab => vfxPrefab;
        public string Key => key;
    }
}

