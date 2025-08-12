using UnityEngine;
using System;

namespace Subscript.ObserveEvent
{
    public class BaseEventHandler
    {
        private event Action EventAction;

        public void Subscribe(Action listener)
        {
            EventAction += listener;
        }

        public void Unsubscribe(Action listener)
        {
            EventAction -= listener;
        }

        public void Trigger()
        {
            EventAction?.Invoke();
        }

        public void UnsubscribeAll()
        {
            if (EventAction != null)
            {
                foreach (var d in EventAction.GetInvocationList())
                {
                    EventAction -= (Action)d;
                }
            }
        }
    }

    public class BaseEventHandler<T>
    {
        public event Action<T> EventAction;

        public void Subscribe(Action<T> listener)
        {
            EventAction += listener;
        }

        public void Unsubscribe(Action<T> listener)
        {
            EventAction -= listener;
        }

        public void Trigger(T value)
        {
            EventAction?.Invoke(value);
        }

        public void UnsubscribeAll()
        {
            if (EventAction != null)
            {
                foreach (var d in EventAction.GetInvocationList())
                {
                    EventAction -= (Action<T>)d;
                }
            }
        }
    }
    public class BaseEventHandler<T1, T2>
    {
        private event Action<T1, T2> EventAction;

        public void Subscribe(Action<T1, T2> listener)
        {
            EventAction += listener;
        }

        public void Unsubscribe(Action<T1, T2> listener)
        {
            EventAction -= listener;
        }

        public void Trigger(T1 arg1, T2 arg2)
        {
            EventAction?.Invoke(arg1, arg2);
        }

        public void UnsubscribeAll()
        {
            if (EventAction != null)
            {
                foreach (var d in EventAction.GetInvocationList())
                {
                    EventAction -= (Action<T1, T2>)d;
                }
            }
        }
    }

}

