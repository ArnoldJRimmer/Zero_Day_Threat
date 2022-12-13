using GD.Engine.Globals;
using Microsoft.Xna.Framework;

namespace GD.Engine
{
    public class CameraFOVController : Component
    {
        private float scrollWheelIncrement;
        private Camera camera;

        public CameraFOVController(float scrollWheelIncrement)
        {
            //let's use ternary operator to validate the input
            this.scrollWheelIncrement = (scrollWheelIncrement == 0) ? 1 : scrollWheelIncrement;

            //BUG
            //camera = gameObject.GetComponent<Camera>();

            //    Application.SoundManager.Play2D("boom");
        }

        public override void Update(GameTime gameTime)
        {
            //NMCG - remove and call in constructor - see BUG above
            camera = gameObject.GetComponent<Camera>();

            //listen for mouse scroll wheel
            int delta = Input.Mouse.GetDeltaFromScrollWheel();

            //if positive, increase camera FOV by scrollWheelMultiplier
            if (delta > 0)
                camera.FieldOfView += MathHelper.ToRadians(scrollWheelIncrement);
            //if negative, decrease camera FOV by scrollWheelMultiplier
            else if (delta < 0)
                camera.FieldOfView -= MathHelper.ToRadians(scrollWheelIncrement);

            base.Update(gameTime);
        }
    }
}