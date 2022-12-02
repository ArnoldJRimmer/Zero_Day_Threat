using GD.Engine.Globals;
using JigLibX.Collision;
using JigLibX.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace GD.Engine.Inputs
{
    /// <summary>
    /// Enumeration of possible mouse buttons
    /// </summary>
    public enum MouseButton : sbyte
    {
        Left, Middle, Right, Any
    }

    /// <summary>
    /// Provides methods to obtain input from the mouse
    /// </summary>
    public class MouseComponent : GameComponent
    {
        private static readonly int BOUNDS_DIMENSION = 2;

        #region Fields

        private MouseState currentState;
        private MouseState previousState;
        protected Vector2 mouseDelta;

        #endregion Fields

        #region Properties

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(currentState.X, currentState.Y, BOUNDS_DIMENSION, BOUNDS_DIMENSION);
            }
        }

        public int X
        {
            get { return currentState.X; }
        }

        public int Y
        {
            get { return currentState.Y; }
        }

        public Vector2 Position
        {
            get { return new Vector2(currentState.X, currentState.Y); }
            set { Mouse.SetPosition((int)value.X, (int)value.Y); }
        }

        public Vector2 PreviousPosition
        {
            get { return new Vector2(previousState.X, previousState.Y); }
        }

        public int Wheel
        {
            get { return currentState.ScrollWheelValue - previousState.ScrollWheelValue; }
        }

        public Vector2 Delta
        {
            get { return mouseDelta; }
        }

        public bool IsMoving
        {
            get { return (currentState.X != previousState.X) || (currentState.Y != previousState.Y); }
        }

        public bool IsDragging(MouseButton button = MouseButton.Left)
        {
            return IsDown(button) && IsMoving;
        }

        #endregion Properties

        #region Constructors

        public MouseComponent(Game game)
         : base(game)
        {
            currentState = Mouse.GetState();
            previousState = currentState;
            mouseDelta = Vector2.Zero;
        }

        #endregion Constructors

        #region Update

        public override void Update(GameTime gameTime)
        {
            previousState = currentState;
            currentState = Mouse.GetState();

            if (Application.Screen.IsCursorLocked)
            {
                mouseDelta.X = (currentState.X - Application.Screen.ScreenCentre.X);
                mouseDelta.Y = (currentState.Y - Application.Screen.ScreenCentre.Y);
            }
            else
            {
                mouseDelta.X = (currentState.X - previousState.X);
                mouseDelta.Y = (currentState.Y - previousState.Y);
            }

            base.Update(gameTime);
        }

        #endregion Update

        #region Actions - Input & Visibility

        /// <summary>
        /// Checks if any button, scrollwheel, or mouse movement has taken place since last update
        /// </summary>
        /// <returns>True if state changed, otherwise false</returns>
        public bool IsStateChanged()
        {
            return (currentState.Equals(previousState)) ? false : true;
        }

        /// <summary>
        /// Gets the current -ve/+ve scroll wheel value
        /// </summary>
        /// <returns>A positive or negative integer</returns>
        public int GetScrollWheelValue()
        {
            return currentState.ScrollWheelValue;
        }

        /// <summary>
        /// Checks if the scroll wheel been moved since the last update
        /// </summary>
        /// <returns>True if the scroll wheel has been moved, otherwise false</returns>
        public int GetDeltaFromScrollWheel()
        {
            if (IsStateChanged()) //if state changed then return difference
            {
                return currentState.ScrollWheelValue - previousState.ScrollWheelValue;
            }

            return 0;
        }

        /// <summary>
        /// Calculates the mouse pointer distance (in X and Y) from the screen centre (e.g. width/2, height/2)
        /// </summary>
        /// <param name="screenCentre">Delta from this screen centre position</param>
        /// <returns>Vector2</returns>
        public Vector2 GetDeltaFromCentre(Vector2 screenCentre)
        {
            return new Vector2(currentState.X - screenCentre.X,
                previousState.Y - screenCentre.Y);
        }

        public virtual bool IsDown(MouseButton button)
        {
            return MouseButtonState(button, ButtonState.Pressed);
        }

        public virtual bool IsUp(MouseButton button = MouseButton.Left)
        {
            return MouseButtonState(button, ButtonState.Released);
        }

        protected virtual bool MouseButtonState(MouseButton button, ButtonState state)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left: result = currentState.LeftButton == state; break;
                case MouseButton.Middle: result = currentState.MiddleButton == state; break;
                case MouseButton.Right: result = currentState.RightButton == state; break;
            }

            return result;
        }

        public virtual bool WasJustClicked(MouseButton button = MouseButton.Left)
        {
            bool clicked = false;

            if (button == MouseButton.Left)
                clicked = currentState.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed;
            else if (button == MouseButton.Middle)
                clicked = currentState.MiddleButton == ButtonState.Released && previousState.MiddleButton == ButtonState.Pressed;
            else if (button == MouseButton.Right)
                clicked = currentState.RightButton == ButtonState.Released && previousState.RightButton == ButtonState.Pressed;

            return clicked;
        }

        public void SetMouseVisible(bool isMouseVisible)
        {
            Game.IsMouseVisible = isMouseVisible;
        }

        #endregion Actions - Input & Visibility

        #region Actions - Ray Picking

        private float frac;
        private CollisionSkin skin;

        /// <summary>
        /// Used when in 1st person collidable camera mode start distance allows us to start the ray outside the
        /// collidable skin of the 1st person colliable camera object, otherwise the only thing we would ever collide
        /// with would be ourselves!
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="cameraTransform"></param>
        /// <param name="screenPosition"></param>
        /// <param name="startDistance"></param>
        /// <param name="endDistance"></param>
        /// <param name="pos"></param>
        /// <param name="normal"></param>
        /// <returns></returns>
        public GameObject GetPickedObject(Camera camera,
            Transform cameraTransform,
            Vector2 screenPosition,
            float startDistance, float endDistance,
            out Vector3 pos, out Vector3 normal)
        {
            Vector3 ray = GetMouseRayDirection(camera, screenPosition);
            ImmovableSkinPredicate pred = new ImmovableSkinPredicate();

            Application.PhysicsManager.PhysicsSystem.CollisionSystem.SegmentIntersect(out frac, out skin, out pos, out normal,
                new Segment(cameraTransform.Translation + startDistance * Vector3.Normalize(ray), ray * endDistance), pred);

            if (skin != null && skin.Owner != null)
                return skin.Owner.Parent as GameObject;

            return null;
        }

        public GameObject GetPickedObject(Camera camera, Transform cameraTransform, float distance, out Vector3 pos, out Vector3 normal)
        {
            return GetPickedObject(camera, cameraTransform, new Vector2(currentState.X, currentState.Y), 0, distance, out pos, out normal);
        }

        public GameObject GetPickedObject(Camera camera, Transform cameraTransform, float startDistance, float distance, out Vector3 pos, out Vector3 normal)
        {
            return GetPickedObject(camera, cameraTransform, new Vector2(currentState.X, currentState.Y), startDistance, distance, out pos, out normal);
        }

        public Vector3 GetMouseRayDirection(Camera camera)
        {
            return GetMouseRayDirection(camera, new Vector2(currentState.X, currentState.Y));
        }

        /// <summary>
        /// Inner class used for ray picking
        /// </summary>
        internal class ImmovableSkinPredicate : CollisionSkinPredicate1
        {
            public override bool ConsiderSkin(CollisionSkin skin0)
            {
                return skin0.Owner != null ? true : false;
            }
        }

        /// <summary>
        /// Get a ray positioned at the mouse's location on the screen - used for picking
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public Microsoft.Xna.Framework.Ray GetMouseRay(Camera camera)
        {
            //get the positions of the mouse in screen space
            Vector3 near = new Vector3(currentState.X, currentState.Y, 0);

            //convert from screen space to world space
            near = camera.ViewPort.Unproject(near, camera.Projection, camera.View, Matrix.Identity);

            return GetMouseRayFromNearPosition(camera, near);
        }

        /// <summary>
        /// Get a ray from a user-defined near position in world space and the mouse pointer
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="near"></param>
        /// <returns></returns>
        public Microsoft.Xna.Framework.Ray GetMouseRayFromNearPosition(Camera camera, Vector3 near)
        {
            //get the positions of the mouse in screen space
            Vector3 far = new Vector3(currentState.X, currentState.Y, 1);

            //convert from screen space to world space
            far = camera.ViewPort.Unproject(far, camera.Projection, camera.View, Matrix.Identity);

            //generate a ray to use for intersection tests
            return new Microsoft.Xna.Framework.Ray(near, Vector3.Normalize(far - near));
        }

        /// <summary>
        /// Get a ray positioned at the screen position - used for picking when we have a centred reticule
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="screenPosition"></param>
        /// <returns></returns>
        public Vector3 GetMouseRayDirection(Camera camera, Vector2 screenPosition)
        {
            //get the positions of the mouse in screen space
            Vector3 near = new Vector3(screenPosition.X, screenPosition.Y, 0);
            //bug with mouse picking - thanks Paudric Smith
            Vector3 far = new Vector3(screenPosition.X, screenPosition.Y, 1);

            //convert from screen space to world space
            near = camera.ViewPort.Unproject(near, camera.Projection, camera.View, Matrix.Identity);
            far = camera.ViewPort.Unproject(far, camera.Projection, camera.View, Matrix.Identity);

            //generate a ray to use for intersection tests
            return Vector3.Normalize(far - near);
        }

        #endregion Actions - Ray Picking
    }
}