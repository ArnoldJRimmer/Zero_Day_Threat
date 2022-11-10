using GD.App;
using GD.Engine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel;
using System.Reflection;

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
        private bool isGrounded;

        #endregion Fields

        #region Temps

        protected Vector3 translation = Vector3.Zero;
        protected Vector3 rotation = Vector3.Zero;

        #endregion Temps

        #region Constructors

        public FirstPersonController(float moveSpeed, float strafeSpeed, float rotationSpeed, bool isGrounded = true)
    : this(moveSpeed, strafeSpeed, rotationSpeed * Vector2.One, isGrounded)
        {
        }

        public FirstPersonController(float moveSpeed, float strafeSpeed, Vector2 rotationSpeed, bool isGrounded = true)
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

            transform.Translate(translation);
        }

        protected virtual void HandleMouseInput(GameTime gameTime)
        {
            rotation = Vector3.Zero;
            var delta = Input.Mouse.Delta;

            #region CameraBounds
            //Stop viewing from left to right 
            if (delta.X >= AppData.STOP_ROTATION_BOUNDS_X)
            {
                //Don't allow the camera to move past this point
                delta.X = AppData.STOP_ROTATION_BOUNDS_X;

            }
            else if (delta.X <= -AppData.STOP_ROTATION_BOUNDS_X)
            {
                delta.X = -AppData.STOP_ROTATION_BOUNDS_X;
            }


            //Stop viewing up and down
            if (delta.Y >= AppData.STOP_ROTATION_BOUNDS_Y)
            {
                //Don't allow the camera to move past this point
                delta.Y = AppData.STOP_ROTATION_BOUNDS_Y;

            }
            else if (delta.Y <= -AppData.STOP_ROTATION_BOUNDS_Y)
            {
                delta.Y = -AppData.STOP_ROTATION_BOUNDS_Y;
            }
            #endregion CamerBounds

            //Q - where are X and Y reversed?
            rotation.Y -= delta.X * rotationSpeed.X * gameTime.ElapsedGameTime.Milliseconds;
            rotation.X -= delta.Y * rotationSpeed.Y * gameTime.ElapsedGameTime.Milliseconds;

            //Need to work on the rotation of the y so that is stops
         


            if (delta.Length() != 0)
            {

            }

                transform.SetRotation(rotation);
        }


        #endregion Actions - Update, Input

        #region Actions - Gamepad (Unused)

        protected virtual void HandleGamepadInput(GameTime gameTime)
        {
        }

        #endregion Actions - Gamepad (Unused)
    }
}