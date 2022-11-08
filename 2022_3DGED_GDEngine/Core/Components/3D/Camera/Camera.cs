using Microsoft.Xna.Framework;

namespace GD.Engine
{
    /// <summary>
    /// Stores the fields required to represent a Camera
    /// </summary>
    public class Camera : Component
    {
        #region Fields

        // private Matrix view;
        private float fieldOfView;

        private float aspectRatio;
        private float nearPlaneDistance;
        private float farPlaneDistance;

        #endregion Fields

        #region Properties

        public float FieldOfView
        {
            get
            {
                return fieldOfView;
            }
            set
            {
                //added validation on FOV to ensure its never set <= zero
                fieldOfView = value >= 0 ? value : MathHelper.PiOver2 / 2;
            }
        }

        public float AspectRatio { get => aspectRatio; set => aspectRatio = value; }
        public float NearPlaneDistance { get => nearPlaneDistance; set => nearPlaneDistance = value >= 0 ? value : 0.1f; }
        public float FarPlaneDistance { get => farPlaneDistance; set => farPlaneDistance = value >= 0 ? value : 100; }

        public Matrix View
        {
            get
            {
                //TODO - improve so not always calculated
                return Matrix.CreateLookAt(transform.translation, transform.translation + transform.World.Forward, transform.World.Up);
            }
        }

        public Matrix Projection
        {
            get
            {
                //TODO - improve so not always calculated
                return Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlaneDistance, farPlaneDistance);
            }
        }

        #endregion Properties

        #region Constructors

        public Camera(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
        {
            FieldOfView = fieldOfView;
            AspectRatio = aspectRatio;
            NearPlaneDistance = nearPlaneDistance;
            FarPlaneDistance = farPlaneDistance;
        }

        #endregion Constructors
    }
}