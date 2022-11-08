using GD.Engine;
using GD.Engine.Parameters;
using Microsoft.Xna.Framework;

namespace GD.App
{
    public class CurveBehaviour : Component
    {
        private Curve3D translationCurve;

        public CurveBehaviour(Curve3D translationCurve)
        {
            this.translationCurve = translationCurve;
        }

        public override void Update(GameTime gameTime)
        {
            double time = gameTime.TotalGameTime.TotalMilliseconds;

            transform.SetTranslation(translationCurve.Evaluate(time, 4));
            // transform.SetRotation(rotationCurve.Evaluate(time, 4));

            base.Update(gameTime);
        }
    }
}