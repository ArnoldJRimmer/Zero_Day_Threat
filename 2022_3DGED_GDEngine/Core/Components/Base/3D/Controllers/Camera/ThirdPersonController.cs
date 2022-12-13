using GD.Engine.Globals;
using Microsoft.Xna.Framework;
using System;

namespace GD.Engine
{
    public class ThirdPersonController : Component
    {
        private GameObject target;

        public ThirdPersonController()
        { }

        public override void Update(GameTime gameTime)
        {
            if (Application.Player != null)
                target = Application.Player;
            else
                throw new ArgumentNullException("Target not set! Do this in main");

            if (target != null)
            {
                //use target position + offset to generate new camera position
                var newPosition = target.Transform.Translation
                    + new Vector3(0, 2, 10);

                //set new camera position
                transform.SetTranslation(newPosition);
            }

            //since parent Update does nothing then dont bother calling it
            //base.Update(gameTime);
        }
    }
}