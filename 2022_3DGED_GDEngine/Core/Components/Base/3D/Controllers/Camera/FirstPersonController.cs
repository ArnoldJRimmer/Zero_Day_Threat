using GD.App;
using GD.Engine.Data;
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
            // translation = Vector3.Zero;
            //
            // if (Input.Keys.IsPressed(Keys.W))
            //     translation += transform.World.Forward * moveSpeed * gameTime.ElapsedGameTime.Milliseconds;
            // else if (Input.Keys.IsPressed(Keys.S))
            //     translation -= transform.World.Forward * moveSpeed * gameTime.ElapsedGameTime.Milliseconds;
            //
            // if (Input.Keys.IsPressed(Keys.A))
            //     translation += transform.World.Left * strafeSpeed * gameTime.ElapsedGameTime.Milliseconds;
            // else if (Input.Keys.IsPressed(Keys.D))
            //     translation += transform.World.Right * strafeSpeed * gameTime.ElapsedGameTime.Milliseconds;
            //
            // if (isGrounded)
            //     translation.Y = 0;
            //
            // transform.Translate(translation);
        }

        protected virtual void HandleMouseInput(GameTime gameTime)
        {
            rotation = new Vector3(0,MathHelper.PiOver2,0);
           
            var delta = Input.Mouse.Delta;

            

            if (delta.X < AppData.boundingScreen.X / 2  && delta.X > -AppData.boundingScreen.X/2 && delta.Y < AppData.boundingScreen.Y/2 && delta.Y > -AppData.boundingScreen.Y/2)
            {
                //Q - where are X and Y reversed?
                rotation.Y -= delta.X * rotationSpeed.X * gameTime.ElapsedGameTime.Milliseconds;
                rotation.X -= delta.Y * rotationSpeed.Y * gameTime.ElapsedGameTime.Milliseconds;

                //Need to work on the rotation of the y so that is stops at 180
                //Also restrict the x so that it doesn't look all the way up
                if (delta.Length() != 0)
                {

                }

                transform.SetRotation(rotation);
            }
        }

        #endregion Actions - Update, Input

        #region Actions - Gamepad (Unused)

        protected virtual void HandleGamepadInput(GameTime gameTime)
        {
        }

        #endregion Actions - Gamepad (Unused)
    }
}