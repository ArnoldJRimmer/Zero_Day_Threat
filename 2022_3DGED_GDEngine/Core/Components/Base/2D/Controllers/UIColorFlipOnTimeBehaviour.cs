using Microsoft.Xna.Framework;

namespace GD.Engine
{
    public class UIColorFlipOnTimeBehaviour : Component
    {
        private Color startColor;
        private Color endColor;
        private float flipIntervalTimeMs;

        //internal
        private float totalElapsedTimeMs;
        private bool flipColor;
        private Material2D material2D;

        public UIColorFlipOnTimeBehaviour(Color startColor,
            Color endColor,
            float flipIntervalTimeMs)
        {
            this.startColor = startColor;
            this.endColor = endColor;
            this.flipIntervalTimeMs = flipIntervalTimeMs;
        }

        public override void Update(GameTime gameTime)
        {
            //get material to access color
            if (material2D == null)
                material2D = gameObject.GetComponent<Renderer2D>().Material;

            totalElapsedTimeMs += gameTime.ElapsedGameTime.Milliseconds;

            if (totalElapsedTimeMs > flipIntervalTimeMs)
            {
                //reset elapsed time
                totalElapsedTimeMs -= flipIntervalTimeMs;

                //flip the control bool
                flipColor = !flipColor;

                //flips between both colors every timeInMs
                material2D.Color = flipColor ? endColor : startColor;
            }

            //base does nothing
            // base.Update();
        }
    }
}