using GD.Engine;
using GD.Engine.Parameters;
using Microsoft.Xna.Framework;
using System;

namespace GD.App
{
    /// <summary>
    /// Applies an action (Action) to a target game object
    /// </summary>
    /// <see cref="Main.InitializeCameras()"/>
    public class CurveBehaviour : Component
    {
        #region Fields

        private Curve3D curve;
        private Action<Curve3D, GameObject, GameTime> action;

        #endregion Fields

        #region Constructors

        public CurveBehaviour(Curve3D curve, Action<Curve3D, GameObject, GameTime> action)
        {
            this.curve = curve;
            this.action = action;
        }

        #endregion Constructors

        public override void Update(GameTime gameTime)
        {
            action(curve, gameObject, gameTime);
            //transform.SetTranslation(curve.Evaluate(time, 4));
            // transform.SetRotation(rotationCurve.Evaluate(time, 4));
        }
    }
}