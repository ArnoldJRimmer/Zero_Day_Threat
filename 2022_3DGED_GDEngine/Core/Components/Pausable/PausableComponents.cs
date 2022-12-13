using GD.Engine.Events;
using Microsoft.Xna.Framework;

namespace GD.Engine
{
    /// <summary>
    /// Creates a class based on the GameComponent class that can be paused when the menu is shown.
    /// </summary>
    public class PausableGameComponent : GameComponent
    {
        #region Fields

        private StatusType statusType;

        #endregion Fields

        #region Properties

        public bool IsUpdated
        {
            get
            {
                return (statusType & StatusType.Updated) != 0;
            }
        }

        public StatusType StatusType
        {
            get
            {
                return statusType;
            }
            set
            {
                statusType = value;
            }
        }

        #endregion Properties

        #region Constructors

        public PausableGameComponent(Game game)
            : this(game, StatusType.Updated)
        {
        }

        public PausableGameComponent(Game game, StatusType statusType)
           : base(game)
        {
            //allows us to start the game component with drawing and/or updating paused
            this.statusType = statusType;

            //subscribe to events that will affect the state of any child class (e.g. a menu play event for the object manager
            SubscribeToEvents();
        }

        #endregion Constructors

        #region Handle Events

        /// <summary>
        /// Subscribe to any events that will affect any child class (e.g. menu pause in ObjectManager)
        /// </summary>
        protected virtual void SubscribeToEvents()
        {
            EventDispatcher.Subscribe(EventCategoryType.Menu, HandleEvent);
        }

        protected virtual void HandleEvent(EventData eventData)
        {
            if (eventData.EventCategoryType == EventCategoryType.Menu)
            {
                if (eventData.EventActionType == EventActionType.OnPause)
                    statusType = StatusType.Off;
                else if (eventData.EventActionType == EventActionType.OnPlay)
                    statusType = StatusType.Updated;
            }
        }

        #endregion Handle Events
    }

    /// <summary>
    /// Creates a class based on the DrawableGameComponent class that can be paused when the menu is shown.
    /// </summary>
    public class PausableDrawableGameComponent : DrawableGameComponent
    {
        #region Fields

        protected StatusType statusType;

        #endregion Fields

        #region Properties

        public bool IsUpdated
        {
            get
            {
                return (statusType & StatusType.Updated) != 0;
            }
        }

        public bool IsDrawn
        {
            get
            {
                return (statusType & StatusType.Drawn) != 0;
            }
        }

        public StatusType StatusType
        {
            get
            {
                return statusType;
            }
            set
            {
                statusType = value;
            }
        }

        #endregion Properties

        #region Constructors

        public PausableDrawableGameComponent(Game game) : this(game, StatusType.Updated | StatusType.Drawn)
        {
        }

        public PausableDrawableGameComponent(Game game, StatusType statusType)
            : base(game)
        {
            //allows us to start the game component with drawing and/or updating paused
            this.statusType = statusType;

            //subscribe to events that will affect the state of any child class (e.g. a menu play event for the object manager
            SubscribeToEvents();
        }

        #endregion Constructors

        #region Handle Events

        /// <summary>
        /// Subscribe to any events that will affect any child class (e.g. menu pause in ObjectManager)
        /// </summary>
        protected virtual void SubscribeToEvents()
        {
            //menu
            EventDispatcher.Subscribe(EventCategoryType.Menu, HandleEvent);
        }

        protected virtual void HandleEvent(EventData eventData)
        {
            if (eventData.EventCategoryType == EventCategoryType.Menu)
            {
                if (eventData.EventActionType == EventActionType.OnPause)
                    statusType = StatusType.Off;
                else if (eventData.EventActionType == EventActionType.OnPlay)
                    statusType = StatusType.Drawn | StatusType.Updated;
            }
        }

        #endregion Handle Events
    }
}