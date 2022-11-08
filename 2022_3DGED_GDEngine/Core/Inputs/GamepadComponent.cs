using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GD.Engine.Inputs
{
    /// <summary>
    /// Provides methods to obtain input from any of the 4 connected game pads
    /// </summary>
    public class GamepadComponent : GameComponent
    {
        #region Fields

        private int gamePadCount = 4;
        private GamePadState[] currentState;
        private GamePadState[] previousState;
        private Vector2 deadZone = new Vector2(0.4f, 0.4f);

        #endregion Fields

        #region Temporary

        private Vector2 tempVector;

        #endregion Temporary

        #region Properties

        public Vector2 DeadZone
        {
            get { return deadZone; }
            set
            {
                if (value.X >= 0.0f && value.Y >= 0.0f)
                    deadZone = value;
            }
        }

        #endregion Properties

        #region Constructors

        public GamepadComponent(Game game)
    : base(game)
        {
            currentState = new GamePadState[4];
            previousState = new GamePadState[4];

            for (int i = 0; i < gamePadCount; i++)
            {
                currentState[i] = GamePad.GetState((PlayerIndex)i);
                previousState[i] = currentState[i];
            }
        }

        #endregion Constructors

        #region Update

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < gamePadCount; i++)
            {
                previousState[i] = currentState[i];
                currentState[i] = GamePad.GetState((PlayerIndex)i);
            }

            base.Update(gameTime);
        }

        #endregion Update

        #region Actions

        public bool IsConnected(PlayerIndex index = PlayerIndex.One)
        {
            return currentState[(int)index].IsConnected;
        }

        public bool IsPressed(Buttons button, PlayerIndex index = PlayerIndex.One)
        {
            return currentState[(int)index].IsButtonDown(button);
        }

        public bool IsReleased(Buttons button, PlayerIndex index = PlayerIndex.One)
        {
            return currentState[(int)index].IsButtonUp(button);
        }

        public bool WasJustPressed(Buttons button, PlayerIndex index = PlayerIndex.One)
        {
            return currentState[(int)index].IsButtonUp(button) && previousState[(int)index].IsButtonDown(button);
        }

        public Vector2 GetAxis(Buttons button, PlayerIndex index = PlayerIndex.One)
        {
            if (button == Buttons.LeftTrigger || button == Buttons.RightTrigger)
            {
                tempVector.X = currentState[(int)index].Triggers.Left;
                tempVector.Y = currentState[(int)index].Triggers.Right;
            }
            else if (button == Buttons.LeftStick)
                tempVector = ThumbSticks(true, index);
            else if (button == Buttons.RightStick)
                tempVector = ThumbSticks(false, index);

            return tempVector;
        }

        public float Triggers(bool left = true, PlayerIndex index = PlayerIndex.One)
        {
            if (left)
                return currentState[(int)index].Triggers.Left;
            else
                return currentState[(int)index].Triggers.Right;
        }

        public Vector2 ThumbSticks(bool left = true, PlayerIndex index = PlayerIndex.One)
        {
            if (left)
                return CheckDeadZone(currentState[(int)index].ThumbSticks.Left);
            else
                return CheckDeadZone(currentState[(int)index].ThumbSticks.Right);
        }

        private Vector2 CheckDeadZone(Vector2 vector)
        {
            tempVector.X = (Math.Abs(vector.X) >= deadZone.X) ? vector.X : 0.0f;
            tempVector.Y = (Math.Abs(vector.Y) >= deadZone.Y) ? vector.Y : 0.0f;
            return tempVector;
        }

        #endregion Actions
    }
}