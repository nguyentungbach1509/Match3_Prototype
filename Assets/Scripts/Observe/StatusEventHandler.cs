using Subscript.ObserveEvent;


namespace SubScript.ObserveEvent
{
    public class StatusEventHandler
    {
        #region Events 

        public class OnDamaged : BaseEventHandler { }
        public class OnHealthChanged : BaseEventHandler<int, bool> { }
        public class OnHealthBarChanged : BaseEventHandler<float> { }

        #endregion

        #region Global Events

        private static readonly OnDamaged globalOnDamaged = new();
        private static readonly OnHealthChanged globalOnHealthChanged = new();


        //Global event accessors

        public static OnDamaged GlobalOnDamagedEvent => globalOnDamaged;
        public static OnHealthChanged GlobalOnHealthChangedEvent => globalOnHealthChanged;

        #endregion

        #region Instance Events
        private readonly OnDamaged onDamaged;
        private readonly OnHealthChanged onHealthChanged;
        private readonly OnHealthBarChanged onHealthBarChanged;

        //Instance event accessors
        public OnDamaged OnDamagedEvent => onDamaged;
        public OnHealthChanged OnHealthChangedEvent => onHealthChanged;
        public OnHealthBarChanged OnHealthBarChangedEvent => onHealthBarChanged;
        

        #endregion

        /// <summary>
        /// Constructor dung de khoi tao event co mot instance cu the
        /// Neu muon su dung event global thi khong can khoi tao
        /// </summary>
        public StatusEventHandler()
        {
            onDamaged = new OnDamaged();
            onHealthChanged = new OnHealthChanged();
            onHealthBarChanged = new OnHealthBarChanged();
        }

        public static void UnsubscribeAllGlobalEvents()
        {
            GlobalOnDamagedEvent.UnsubscribeAll();
            GlobalOnHealthChangedEvent.UnsubscribeAll();
        }

        public void UnsubscribeAllInstanceEvents()
        {
            onDamaged.UnsubscribeAll();
            onHealthChanged.UnsubscribeAll();
        }
    }
}

