using GD.Core;
using GD.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine.Globals
{
    /// <summary>
    /// Static class that contains global objects used in the engine.
    /// </summary>
    public class Application  //TODO - : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the main game
        /// </summary>
        public static Game Main { get; set; }

        /// <summary>
        /// Gets or sets the content manager.
        /// </summary>
        public static ContentManager Content { get; set; }

        /// <summary>
        /// Gets or sets the graphics device.
        /// </summary>
        public static GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        /// Gets or sets the graphics device manager
        /// </summary>
        public static GraphicsDeviceManager GraphicsDeviceManager { get; set; }

        /// <summary>
        /// Gets or sets the camera manager.
        /// </summary>
        public static CameraManager CameraManager { get; set; }

        /// <summary>
        /// Gets or sets the scene manager.
        /// </summary>
        public static SceneManager<Scene> SceneManager { get; set; }

        /// <summary>
        /// Gets or sets the scene manager.
        /// </summary>
        public static SoundManager SoundManager { get; set; }

        /// <summary>
        /// Gets or sets the screen object that allows us to change resolution
        /// </summary>
        public static Screen Screen { get; set; }

        /// <summary>
        /// Store a handle to currently active player
        /// </summary>
        public static GameObject Player { get; set; }

        /// <summary>
        /// Store a handle for the physics manager so that collider components can access Space to add BEPU collider primitives (e.g. Box, Sphere, Capsule)
        /// </summary>
        public static PhysicsManager PhysicsManager { get; internal set; }

        /// <summary>
        /// For the UI
        /// </summary>
        public static SceneManager<Scene2D> UISceneManager { get; internal set; }

        #endregion Properties
    }
}