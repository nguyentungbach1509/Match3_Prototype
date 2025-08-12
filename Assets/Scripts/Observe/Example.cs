using SubScript.ObserveEvent;
using UnityEngine;

namespace Subscript.ObserveEvent
{
    public class Example : MonoBehaviour
    {
        //Public instance event
        public StatusEventHandler statusEventHandler;

        public void Init()
        {
            //public instance event thi phai khoi tao
            statusEventHandler = new StatusEventHandler();
        }

        /// <summary>
        /// Cach dung event instance public
        /// </summary>
        /// <param name="dmg"></param>
        public void OnTakeDamagePublic(int dmg)
        {
            statusEventHandler.OnHealthChangedEvent.Trigger(dmg, true);
        }

        /// <summary>
        /// Cach dung global event
        /// </summary>
        /// <param name="dmg"></param>
        public void OnTakeDamageGlobal(int dmg)
        {
            StatusEventHandler.GlobalOnHealthChangedEvent.Trigger(dmg, true);
        }
    }

    public class SubscribeEventExample
    {
        private Example example;

        //Subscribe instance public event
        public void InstanceSubscribeEvents()
        {
            example.statusEventHandler.OnHealthChangedEvent.Subscribe(HealthChangedPublic);
        }

        //Subscribe global event
        public void GlobalSubscribeEvents()
        {
            StatusEventHandler.GlobalOnHealthChangedEvent.Subscribe(HealthChangedPublic);
        }

        private void HealthChangedPublic(int dmg, bool isBool)
        {
            Debug.Log("Health Change");
        }
    }
}

