using Microsoft.Xna.Framework;
using System;

namespace GD.Engine
{
    /// <summary>
    /// Cycles GameObject through displacement in space at user-defined speed
    /// </summary>
    /// <see cref="CycledRotationBehaviour.Update(Microsoft.Xna.Framework.GameTime)"/>
    public class CycledTranslationBehaviour : Component
    {
        private float maxDisplacement;
        private float angularSpeedMultiplier;
        private Vector3 originalTranslation;

        //constructor (speed, maxDisplacement)
        public CycledTranslationBehaviour(float maxDisplacement, float angularSpeedMultiplier)
        {
            this.maxDisplacement = maxDisplacement;
            this.angularSpeedMultiplier = angularSpeedMultiplier;

            //originalTranslation = transform.translation;
        }

        //override update - transform (translation)
        public override void Update(GameTime gameTime)
        {
            double t = gameTime.TotalGameTime.TotalSeconds;
            t %= 360;

            //get a value which cycles between [-maxDisplacement, maxDisplacement]
            var currentDisplacement = maxDisplacement * Math.Round(
                Math.Sin(MathHelper.ToRadians((float)
               ((int)angularSpeedMultiplier * t))), 2);

            //  System.Diagnostics.Debug.WriteLine(currentDisplacement);

            //add to the transform.translation
            transform.Translate(0, (float)currentDisplacement, 0);

            base.Update(gameTime);
        }
    }
}