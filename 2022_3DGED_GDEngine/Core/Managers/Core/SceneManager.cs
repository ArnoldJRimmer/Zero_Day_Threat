using GD.Engine.Events;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GD.Engine.Managers
{
    /// <summary>
    /// Stores all scenes and updates the active scene
    /// </summary>
    public class SceneManager<T> : PausableGameComponent where T : IUpdateable
    {
        #region Fields

        private T activeScene;
        private Dictionary<string, T> scenes;

        #endregion Fields

        #region Properties

        public T ActiveScene
        {
            get
            {
                if (activeScene == null)
                    throw new NullReferenceException("Active scene not set! Call SetActiveScene()");

                return activeScene;
            }
        }

        #endregion Properties

        #region Constructors

        public SceneManager(Game game)
            : base(game)
        {
            scenes = new Dictionary<string, T>();
        }

        #endregion Constructors

        protected override void SubscribeToEvents()
        {
            //handle add/remove events
            EventDispatcher.Subscribe(EventCategoryType.GameObject, HandleGameObjectEvents);

            base.SubscribeToEvents();
        }

        protected void HandleGameObjectEvents(EventData eventData)
        {
            switch (eventData.EventActionType)
            {
                case EventActionType.OnRemoveObject: //TODO
                    break;

                case EventActionType.OnAddObject: //TODO
                    break;

                default:
                    break;
                    //add more cases for each method that we want to support with events
            }

            //call base method because we want to participate in the pause/play events
            base.HandleEvent(eventData);
        }

        #region Actions - Add, SetActiveScene

        public bool Add(string id, T scene)
        {
            id = id.Trim().ToLower();

            if (scenes.ContainsKey(id))
                return false;

            scenes.Add(id, scene);
            return true;
        }

        public T SetActiveScene(string id)
        {
            id = id.Trim().ToLower();

            if (scenes.ContainsKey(id))
                activeScene = scenes[id];

            return activeScene;
        }

        #endregion Actions - Add, SetActiveScene

        #region Actions - Update

        public override void Update(GameTime gameTime)
        {
            if (IsUpdated)
                activeScene?.Update(gameTime);
        }

        #endregion Actions - Update
    }
}