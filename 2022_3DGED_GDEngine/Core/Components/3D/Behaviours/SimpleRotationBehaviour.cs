using Microsoft.Xna.Framework;

namespace GD.Engine
{
    public class SimpleRotationBehaviour : Component
    {
        #region Fields

        private Vector3 rotationAxis = Vector3.UnitY;
        private float rotationSpeedInRadians;

        #endregion Fields

        #region Constructors

        public SimpleRotationBehaviour(Vector3 rotationAxis, float rotationSpeedInRadians)
        {
            this.rotationAxis = rotationAxis;
            this.rotationSpeedInRadians = rotationSpeedInRadians;
        }

        #endregion Constructors

        #region Actions - Update

        public override void Update(GameTime gameTime)
        {
            //stuff
            //transform.rotation = transform.rotation + new Vector3(0, MathHelper.ToRadians(1), 0);

            transform.rotation = transform.rotation + gameTime.ElapsedGameTime.Milliseconds * rotationSpeedInRadians * rotationAxis;

            //dont call parent since its Update does nothing
            //base.Update(gameTime);
        }

        #endregion Actions - Update
    }
}