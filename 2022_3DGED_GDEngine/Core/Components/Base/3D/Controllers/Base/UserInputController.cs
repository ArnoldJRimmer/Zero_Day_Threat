/*
Function: 		Parent class for all controllers which accept keyboard input and apply to an actor (e.g. a FirstPersonCameraController inherits from this class).
Author: 		NMCG
Version:		1.0
Date Updated:	17/8/17
Bugs:			None
Fixes:			None
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GD.Engine
{
    public class UserInputController : Component
    {
        #region Fields

        private Keys[] moveKeys;
        private float moveSpeed, strafeSpeed, rotationSpeed;

        #endregion Fields

        #region Properties

        public Keys[] MoveKeys { get => moveKeys; set => moveKeys = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float StrafeSpeed { get => strafeSpeed; set => strafeSpeed = value; }
        public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

        #endregion Properties

        public UserInputController(Keys[] moveKeys,
            float moveSpeed, float strafeSpeed, float rotationSpeed)
        {
            MoveKeys = moveKeys;
            MoveSpeed = moveSpeed;
            StrafeSpeed = strafeSpeed;
            RotationSpeed = rotationSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            HandleMouseInput(gameTime);
            HandleKeyboardInput(gameTime);
            HandleGamePadInput(gameTime);
        }

        public virtual void HandleGamePadInput(GameTime gameTime)
        {
        }

        public virtual void HandleMouseInput(GameTime gameTime)
        {
        }

        public virtual void HandleKeyboardInput(GameTime gameTime)
        {
        }

        //Add Equals, Clone, ToString, GetHashCode...
    }
}