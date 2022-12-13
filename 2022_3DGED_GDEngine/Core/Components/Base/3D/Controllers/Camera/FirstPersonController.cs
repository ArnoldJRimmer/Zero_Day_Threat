using GD.Engine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GD.Engine
{
    /// <summary>
    /// Adds simple non-collidable 1st person controller to camera using keyboard and mouse input
    /// </summary>
    public class FirstPersonController : Component
    {
        #region Fields

        protected float moveSpeed = 0.05f;
        protected float strafeSpeed = 0.025f;
        protected Vector2 rotationSpeed;
        private float smoothFactor;
        private bool isGrounded;

        #endregion Fields

        #region Temps

        protected Vector3 translation = Vector3.Zero;
        protected Vector3 rotation = Vector3.Zero;
        private Vector2 oldDelta;

        #endregion Temps

        #region Constructors

        public FirstPersonController(float moveSpeed, float strafeSpeed, float rotationSpeed, float smoothFactor = 0.25f, bool isGrounded = true)
    : this(moveSpeed, strafeSpeed, rotationSpeed * Vector2.One, smoothFactor, isGrounded)
        {
        }

        public FirstPersonController(float moveSpeed, float strafeSpeed, Vector2 rotationSpeed, float smoothFactor, bool isGrounded)
        {
            this.moveSpeed = moveSpeed;
            this.strafeSpeed = strafeSpeed;
            this.rotationSpeed = rotationSpeed;
            this.smoothFactor = smoothFactor;
            this.isGrounded = isGrounded;
        }

        #endregion Constructors

        #region Actions - Update, Input

        public override void Update(GameTime gameTime)
        {
            HandleMouseInput(gameTime);
            HandleKeyboardInput(gameTime);
        }

        protected virtual void HandleKeyboardInput(GameTime gameTime)
        {
            translation = Vector3.Zero;

            if (Input.Keys.IsPressed(Keys.W))
                translation += transform.World.Forward * moveSpeed * gameTime.ElapsedGameTime.Milliseconds;
            else if (Input.Keys.IsPressed(Keys.S))
                translation -= transform.World.Forward * moveSpeed * gameTime.ElapsedGameTime.Milliseconds;

            if (Input.Keys.IsPressed(Keys.A))
                translation += transform.World.Left * strafeSpeed * gameTime.ElapsedGameTime.Milliseconds;
            else if (Input.Keys.IsPressed(Keys.D))
                translation += transform.World.Right * strafeSpeed * gameTime.ElapsedGameTime.Milliseconds;

            if (isGrounded)
                translation.Y = 0;

            //actually apply the movement
            transform.Translate(translation);
        }

        protected virtual void HandleMouseInput(GameTime gameTime)
        {
            rotation = Vector3.Zero;
            var currentDelta = Input.Mouse.Delta;

            //smooth camera movement
            var newDelta = oldDelta.Lerp(currentDelta, smoothFactor);

            //did we move mouse?
            if (newDelta.Length() != 0)
            {
                //Q - where are X and Y reversed?
                rotation.Y -= newDelta.X * rotationSpeed.X * gameTime.ElapsedGameTime.Milliseconds;
                rotation.X -= newDelta.Y * rotationSpeed.Y * gameTime.ElapsedGameTime.Milliseconds;
                transform.SetRotation(rotation);
            }
            //store current to be used for next update of smoothing
            oldDelta = newDelta;
        }

        #endregion Actions - Update, Input

        #region Actions - Gamepad (Unused)

        protected virtual void HandleGamepadInput(GameTime gameTime)
        {
        }

        #endregion Actions - Gamepad (Unused)
    }
}