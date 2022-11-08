using Microsoft.Xna.Framework;
using System;

namespace GD.Engine
{
    /// <summary>
    /// Causes the attached GameObject to rotate at a user defined
    /// speed to a max and min angle in degrees along a user-defined axis
    /// </summary>
    public class CycledRotationBehaviour : Component
    {
        private static readonly int ROUND_PRECISION = 4;
        private Vector3 rotationAxis;
        private float maxAngleInDegrees;
        private float angularSpeedMultiplier;
        private TurnDirectionType turnDirectionType;

        public CycledRotationBehaviour(
            Vector3 rotationAxis,
            float maxAngleInDegrees,
            float angularSpeedMultiplier,
            TurnDirectionType turnDirectionType)
        {
            this.rotationAxis = Vector3.Normalize(rotationAxis);
            this.maxAngleInDegrees = maxAngleInDegrees;
            this.angularSpeedMultiplier = angularSpeedMultiplier;
            this.turnDirectionType = turnDirectionType;
        }

        public override void Update(GameTime gameTime)
        {
            double t = gameTime.TotalGameTime.TotalSeconds;
            t %= 360;
            double angleInDegrees = maxAngleInDegrees * Math.Round(
                Math.Sin(MathHelper.ToRadians((float)
               ((int)turnDirectionType * angularSpeedMultiplier * t))), ROUND_PRECISION);

            var rotation = rotationAxis * MathHelper.ToRadians((float)angleInDegrees);
            transform.SetRotation(rotation);

            base.Update(gameTime);
        }
    }
}