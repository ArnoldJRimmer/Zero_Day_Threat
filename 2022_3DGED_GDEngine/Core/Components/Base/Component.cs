using Microsoft.Xna.Framework;

namespace GD.Engine
{
    /// <summary>
    /// A part of a game object e.g. Mesh, MeshRenderer, Camera, FirstPersonController
    /// </summary>
    public abstract class Component
    {
        #region Fields

        /// <summary>
        /// Friendly name for the current component
        /// </summary>
        public string name;

        /// <summary>
        /// Parent GameObject for the component
        /// </summary>
        public GameObject gameObject;

        /// <summary>
        /// Transform of the parent GameObject for the component
        /// </summary>
        public Transform transform;

        #endregion Fields

        #region Actions - Awake, Enable, Disable, Update

        //public virtual void Awake() { }
        //public virtual void OnEnable() { }
        //public virtual void OnDisable() { }

        public virtual void Update(GameTime gameTime)
        {
            //Overridden in child classes
        }

        #endregion Actions - Awake, Enable, Disable, Update
    }
}