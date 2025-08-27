using Match3.Subscripts;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusCollectSO", menuName = "Data/Status/StatusCollectSO")]
public class StatusCollectSO : ScriptableObject
{
    [SerializeField] List<StatusData> listData;
    private Dictionary<ECellType, StatusData> dictData = new Dictionary<ECellType, StatusData>();

    public StatusData GetData(ECellType key)
    {
        if(dictData.TryGetValue(key, out var value)) return value;
        StatusData data = listData.Find(dt => dt.Type == key);
        dictData[key] = data;
        return data;
    }
}
