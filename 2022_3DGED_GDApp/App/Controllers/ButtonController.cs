using GD.Engine;
using GD.Engine.Events;
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
            object[] onButton = { "startupline" };
            EventData firstLine = new EventData(EventCategoryType.Sound, EventActionType.OnPlay2D,onButton);
            //onButton[1] = "checkingterminal";

            Vector2 delta = Input.Mouse.Delta;
            var mouse = Mouse.GetState();

            if (mouse.X == 146 && mouse.Y == 437)
            {
                isPressed = true;
                if(mouse.LeftButton == ButtonState.Pressed && isPressed == true)
                {
                    EventDispatcher.Raise(firstLine);
                    isPressed = false;
                }

            }
            
            if(mouse.LeftButton == ButtonState.Released)
            {
                isPressed = false;
                Application.SoundManager.Stop("startupLine");
            }


        }

        protected virtual void PlaySoundsInOrder()
        {

        }


    }
}