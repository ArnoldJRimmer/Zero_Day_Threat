using Microsoft.Xna.Framework;

namespace GD.Engine
{
    public class SimpleRotationBehaviour : Component
    {
        #region Fields

        private Vector3 rotationAxis = Vector3.UnitY;
        private float rotationSpeedInDegrees;

        #endregion Fields

        #region Constructors

        public SimpleRotationBehaviour(Vector3 rotationAxis, float rotationSpeedInDegrees)
        {
            this.rotationAxis = rotationAxis;
            this.rotationSpeedInDegrees = rotationSpeedInDegrees;
        }

        #endregion Constructors

        #region Actions - Update

        public override void Update(GameTime gameTime)
        {
            transform.Rotate(gameTime.ElapsedGameTime.Milliseconds * rotationSpeedInDegrees * rotationAxis);

            //dont call parent since its Update does nothing
            //base.Update(gameTime);
        }

        #endregion Actions - Update
    }
}