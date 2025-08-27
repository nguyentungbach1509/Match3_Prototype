using Match3.Subscripts;
using Match3.SubScripts;
using UnityEngine;

namespace Match3.Scripts.Data
{
    public class DataManager : Singleton<DataManager>
    {
        [SerializeField] StatusCollectSO statusData;
        public StatusData GetStatus(ECellType key) => statusData.GetData(key);
    }

}
