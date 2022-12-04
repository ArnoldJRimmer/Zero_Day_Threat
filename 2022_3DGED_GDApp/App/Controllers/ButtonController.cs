using GD.Engine;
using GD.Engine.Globals;
using GD.Engine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Transactions;

namespace GD.Engine
{
    /// <summary>
    /// Added a simple controller that i attached to the cube
    /// </summary>
    public class ButtonController : Component
    {
        #region Fields
        private bool isPressed = false;
        private GameObject myGameObject;
        #endregion Fields

        #region Constructors

        public ButtonController(GameObject gb)
        {
            this.myGameObject = gb;
        }


        #endregion Constructors


        public override void Update(GameTime gameTime)
        {

            HandleMouseInput(gameTime);

        }

        protected virtual void HandleMouseInput(GameTime gameTime)
        {

            Vector2 delta = Input.Mouse.Delta;
            var mouse = Mouse.GetState();

            if (mouse.X == 146 && mouse.Y == 437)
            {
                isPressed = true;
                if(mouse.LeftButton == ButtonState.Pressed && isPressed == true)
                {
                    Application.SoundManager.Play2D("startupline");
                    //Application.SoundManager.Pause("startupline");
                    isPressed = false;
                }

                
            }
            
            if(mouse.LeftButton == ButtonState.Released)
            {
                isPressed = false;
                Application.SoundManager.Stop("startupLine");
            }

            

                    }


    }
}