using GD.Engine;
using GD.Engine.Parameters;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    /// <summary>
    /// Rail controller constrains an actor to movement along a rail and causes the actor to focus on a target.
    /// </summary>
    public class RailController : Component
    {
        #region Fields

        private Rail rail;
        private GameObject target;
        private bool bFirstUpdate = true;

        #endregion Fields

        #region Properties

        public GameObject Target { get => target; set => target = value; }
        public Rail Rail { get => rail; set => rail = value; }

        #endregion Properties

        #region Constructors

        public RailController(string id, Rail rail, GameObject target)
        {
            this.rail = rail;
            this.target = target;
        }

        #endregion Constructors

        public override void Update(GameTime gameTime)
        {
            if (target != null)
            {
                if (bFirstUpdate)
                {
                    //set the initial position of the camera
                    transform.SetTranslation(rail.MidPoint);
                    bFirstUpdate = false;
                }

                //get look vector to target and round to prevent floating-point precision errors across updates
                Vector3 cameraToTarget = gameObject.GetNormalizedVectorTo(target);
                cameraToTarget.Round(3);

                //new position for camera if it is positioned between start and the end points of the rail
                Vector3 projectedCameraPosition = transform.Translation + Vector3.Dot(cameraToTarget, rail.Look) * rail.Look;
                projectedCameraPosition.Round(3);

                //do not allow the camera to move outside the rail
                if (rail.InsideRail(projectedCameraPosition))
                    transform.SetTranslation(projectedCameraPosition);

                //TODO - set the camera to look at the object
            }
        }
    }
}