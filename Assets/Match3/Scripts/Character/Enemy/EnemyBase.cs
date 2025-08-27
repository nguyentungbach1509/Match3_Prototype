using Scripts;
using UnityEngine;

namespace Match3.Scripts.Character
{
    public class EnemyBase : CharacterBase
    {
        [SerializeField] protected AnimationController anim;

        private float time;
        protected StateController state;
        

        public AnimationController Anim => anim;
        public StateController State => state;


        public override void Init()
        {
            base.Init();
            state = new StateController(this);
            time = Random.Range(3f, 10f);
        }

        //Test Damage
        public void UpdateState()
        {
            if (canvas.StatusCtrl.GotFrozen()) return;
            if (time <= 0)
            {
                player.Stats.TakeDamage(stats.Damage);
                time = Random.Range(3f, 10f);
            }
            time -= Time.deltaTime;
        }
    }
}

