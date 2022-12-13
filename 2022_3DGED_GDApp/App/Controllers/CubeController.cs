using GD.Engine;
using GD.Engine.Globals;
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

            if (Input.Keys.WasJustPressed(cubeSelected))
            {
                //transform.rotation = transform.rotation + gameTime.ElapsedGameTime.Milliseconds * rotationSpeedInRadians * rotationAxis;
                //Rotates 90 degrees on key press, no holding

                transform.Rotate(new Vector3(0, 0, 90));

                // If the cube rotation is 360 degrees it resets back to 0 degrees 
                if (transform.Rotation == new Vector3(0, 0,360))
                {
                    transform.SetRotation(new Vector3(0, 0, 0));
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