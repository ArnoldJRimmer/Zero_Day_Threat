using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GD.Engine.Inputs
{
    /// <summary>
    /// Provides methods to obtain input from the keyboard
    /// </summary>
    public sealed class KeyboardComponent : GameComponent
    {
        #region Fields

        private KeyboardState currentState;
        private KeyboardState previousState;

        #endregion Fields

        #region Constructors

        public KeyboardComponent(Game game)
         : base(game)
        {
            currentState = Keyboard.GetState();
            previousState = currentState;
        }

        #endregion Constructors

        #region Update

        public override void Update(GameTime gameTime)
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
            base.Update(gameTime);
        }

        #endregion Update

        #region Actions

        public bool IsPressed(Keys key)
        {
            return currentState.IsKeyDown(key);
        }

        public bool IsReleased(Keys key)
        {
            return currentState.IsKeyUp(key);
        }

        public bool WasJustPressed(Keys key)
        {
            return currentState.IsKeyUp(key) && previousState.IsKeyDown(key);
        }

        #endregion Actions
    }
}