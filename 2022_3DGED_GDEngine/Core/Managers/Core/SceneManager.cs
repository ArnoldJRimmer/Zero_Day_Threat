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
            if (StatusType != StatusType.Off)
                activeScene.Update(gameTime);
        }

        #endregion Actions - Update
    }
}