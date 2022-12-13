using GD.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Transactions;

namespace GD.Engine
{
    /// <summary>
    /// Added a simple controller that i attached to the cube
    /// </summary>
    public class CubeController : Component
    {
        #region Fields

        protected Vector3 rotationAxis = Vector3.UnitY;
        protected Vector3 rotation = Vector3.Zero;
        private float rotationSpeedInRadians;
        Keys cubeSelected;

        #endregion Fields

        #region Constructors

        public CubeController(Vector3 rotationAxis, float rotationSpeedInRadians, Keys key)
        {

            this.rotationAxis = rotationAxis;
            this.rotationSpeedInRadians = rotationSpeedInRadians;
            this.cubeSelected = key;
        }


        #endregion Constructors


        public override void Update(GameTime gameTime)
        {

            HandleKeyboardInput(gameTime);
            HandleMouseInput(gameTime);


        }

        protected virtual void HandleKeyboardInput(GameTime gameTime)
        {

            if (Input.Keys.IsPressed(cubeSelected,true))
            {
                //transform.rotation = transform.rotation + gameTime.ElapsedGameTime.Milliseconds * rotationSpeedInRadians * rotationAxis;
                //Rotates 90 degrees on key press, no holding
                transform.rotation += new Vector3(0, 0, MathHelper.PiOver2);

                // If the cube rotation is 360 degrees it resets back to 0 degrees 
                if (transform.rotation == new Vector3(0, 0, MathHelper.TwoPi))
                {
                    transform.rotation = new Vector3(0, 0, 0);
                }

            }


        }

        protected virtual void HandleMouseInput(GameTime gameTime)
        {

            Vector2 delta = Input.Mouse.Delta;
            var mouse = Mouse.GetState();



        }


    }
}