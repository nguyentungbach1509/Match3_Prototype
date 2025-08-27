using UnityEngine;

namespace Match3.Scripts.Character
{
    public class CharacterCanvas : MonoBehaviour
    {
        [SerializeField] Healthbar hpBar;
        [SerializeField] StatusController statusController;
        public Healthbar HpBar => hpBar;
        public StatusController StatusCtrl => statusController;
        
    }
}

