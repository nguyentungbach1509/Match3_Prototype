using UnityEngine;
namespace Match3.Scripts.Character
{
    public enum EStatus
    {
        Burn, Stun
    }

    public class Status
    {
        private float damage;
        private float time;
        private float cdTime;
        private EStatus status;
        private Sprite sprite;

        public float Damage => damage;
        public float Time => time;
        public EStatus Type => status;
        public Sprite Sprite => sprite;
        
        public Status(StatusSO data)
        {
            damage = data.Damage;
            time = data.Time;
            cdTime = time;
            status = data.StatusType;
            sprite = data.StatusIcon;
        }
    }
}

