using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GD.Engine.Managers
{
    /// <summary>
    /// Stores all scenes and updates the active scene
    /// </summary>
    public class SceneManager
    {
        #region Fields

        private Scene activeScene = null;
        private Dictionary<string, Scene> scenes;

        #endregion Fields

        #region Properties

        public Scene ActiveScene
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

        public SceneManager()
        {
            scenes = new Dictionary<string, Scene>();
        }

        #endregion Constructors

        #region Actions - Add, SetActiveScene

        public bool Add(string id, Scene scene)
        {
            id = id.Trim().ToLower();

            if (scenes.ContainsKey(id))
                return false;

            scenes.Add(id, scene);
            return true;
        }

        public Scene SetActiveScene(string id)
        {
            id = id.Trim().ToLower();

            if (scenes.ContainsKey(id))
                activeScene = scenes[id];

            return activeScene;
        }

        #endregion Actions - Add, SetActiveScene

        #region Actions - Update

        public virtual void Update(GameTime gameTime)
        {
            activeScene.Update(gameTime);
        }

        #endregion Actions - Update
    }
}