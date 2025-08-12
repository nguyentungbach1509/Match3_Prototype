using UnityEngine;

namespace Match3.Scripts.Character
{
    public class CharacterCanvas : MonoBehaviour
    {
        [SerializeField] Healthbar hpBar;
        public Healthbar HpBar => hpBar;
    }
}

