using GD.Engine.Globals;
using Microsoft.Xna.Framework;

namespace GD.Core
{
    /// <summary>
    ///  A singleton class which provides access to methods relating to screen resolution, mouse visibility
    /// </summary>
    public sealed class Screen
    {
        #region Fields

        private Rectangle screenRectangle;
        private Vector2 screenCentre;

        #endregion Fields

        #region Properties

        public Rectangle ScreenRectangle
        {
            get => screenRectangle;
            set
            {
                screenRectangle = value;
                screenCentre = new Vector2(screenRectangle.Width / 2, screenRectangle.Height / 2);
            }
        }

        public Vector2 ScreenCentre { get => screenCentre; }

        //TODO - add code to show/hide mouse based on cursor lock
        public bool IsCursorLocked { get; set; }

        public bool IsMouseVisible
        {
            get { return Application.Main.IsMouseVisible; }
            set { Application.Main.IsMouseVisible = value; }
        }

        #endregion Properties

        #region Constructors

        public Screen()
        {
        }

        #endregion Constructors

        #region Actions

        /// <summary>
        /// Sets common screen parameters at any point in the game (e.g. resolution, mouse visibility)
        /// </summary>
        /// <param name="width">Screen width</param>
        /// <param name="height">Screen height</param>
        /// <param name="isMouseVisible"></param>
        /// <param name="isCursorLocked"></param>
        public void Set(Vector2 resolution, bool isMouseVisible, bool isCursorLocked)
        {
            int width = (int)resolution.X;
            int height = (int)resolution.Y;

            ScreenRectangle = new Rectangle(0, 0, width, height);
            IsCursorLocked = isCursorLocked;
            IsMouseVisible = isMouseVisible;

            Application.GraphicsDeviceManager.PreferredBackBufferWidth = width;
            Application.GraphicsDeviceManager.PreferredBackBufferHeight = height;
            Application.GraphicsDeviceManager.ApplyChanges();

            //TODO - raise event screen size changed
        }

        /// <summary>
        /// Allow us to toggle full screen on/off
        /// </summary>
        public void ToggleFullscreen()
        {
            Application.GraphicsDeviceManager.ToggleFullScreen();
        }

        #endregion Actions
    }
}