using UnityEngine;

namespace Match3.Scripts.Character
{
    public enum ECharacterType
    {
        Enemy, Player
    }


    [CreateAssetMenu(fileName = "StatsData", menuName = "CharacterSO/StatsData")]
    public class StatsData : ScriptableObject
    {
        [SerializeField] int id;
        [SerializeField] string nameKey;
        [SerializeField] float maxHp;
        [SerializeField] float damage;
        [SerializeField] ECharacterType characterType;
        
        public int Id => id;
        public string NameKey => nameKey;
        public float MaxHp => maxHp;
        public float Damage => damage;
        public ECharacterType CharacterType => characterType;
    }
}


