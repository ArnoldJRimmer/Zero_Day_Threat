using GD.Engine;
using GD.Engine.Globals;
using GD.Engine.Inputs;
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
    public class ButtonController : Component
    {
        #region Fields
        private bool isPressed;
        #endregion Fields

        #region Constructors

        public ButtonController()
        {
            
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
                Application.SoundManager.Play2D("startupline");
            }
            

            

            System.Diagnostics.Debug.WriteLine(mouse);
        }


    }
}