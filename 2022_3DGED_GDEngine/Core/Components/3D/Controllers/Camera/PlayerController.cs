using GD.Engine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GD.Engine
{
    /// <summary>
    /// Adds simple non-collidable player controller using keyboard
    /// </summary>
    public class PlayerController : Component
    {
        #region Fields

        protected float moveSpeed = 0.05f;
        protected float strafeSpeed = 0.025f;
        protected Vector2 rotationSpeed;
        private bool isGrounded;

        #endregion Fields

        #region Temps

        protected Vector3 translation = Vector3.Zero;
        protected Vector3 rotation = Vector3.Zero;

        #endregion Temps

        #region Constructors

        public PlayerController(float moveSpeed, float strafeSpeed, float rotationSpeed, bool isGrounded = true)
    : this(moveSpeed, strafeSpeed, rotationSpeed * Vector2.One, isGrounded)
        {
        }

        public PlayerController(float moveSpeed, float strafeSpeed, Vector2 rotationSpeed, bool isGrounded = true)
        {
            this.moveSpeed = moveSpeed;
            this.strafeSpeed = strafeSpeed;
            this.rotationSpeed = rotationSpeed;
            this.isGrounded = isGrounded;
        }

        #endregion Constructors

        #region Actions - Update, Input

        public override void Update(GameTime gameTime)
        {
            HandleKeyboardInput(gameTime);
        }

        protected virtual void HandleKeyboardInput(GameTime gameTime)
        {
            translation = Vector3.Zero;

            if (Input.Keys.IsPressed(Keys.U))
                translation += transform.World.Forward * moveSpeed * gameTime.ElapsedGameTime.Milliseconds;
            else if (Input.Keys.IsPressed(Keys.J))
                translation -= transform.World.Forward * moveSpeed * gameTime.ElapsedGameTime.Milliseconds;

            //if (Input.Keys.IsPressed(Keys.H))
            //   // transform.Rotate();
            //else if (Input.Keys.IsPressed(Keys.K))
            //   // transform.Rotate();

            if (isGrounded)
                translation.Y = 0;

            //actually apply the movement
            transform.Translate(translation);
        }

        #endregion Actions - Update, Input

        #region Actions - Gamepad (Unused)

        protected virtual void HandleMouseInput(GameTime gameTime)
        {
        }
        protected virtual void HandleGamepadInput(GameTime gameTime)
        {
        }

        #endregion Actions - Gamepad (Unused)
    }
}