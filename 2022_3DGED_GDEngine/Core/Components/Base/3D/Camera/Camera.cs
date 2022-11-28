using GD.Engine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    /// <summary>
    /// Stores the fields required to represent a Camera
    /// </summary>
    public class Camera : Component
    {
        #region Fields

        private float fieldOfView;
        private float aspectRatio;
        private float nearPlaneDistance;
        private float farPlaneDistance;

        /// <summary>
        /// Supports orthographic or perspective projection
        /// </summary>
        private CameraProjectionType projectionType;

        /// <summary>
        /// Supports user-defined viewports (e.g. splitscreen)
        /// </summary>
        private Viewport viewPort;

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
        public CameraProjectionType ProjectionType { get => projectionType; set => projectionType = value; }
        public Viewport ViewPort { get => viewPort; set => viewPort = value; }

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
                var projectionMatrix =
                    projectionType == CameraProjectionType.Perspective
                    ? Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlaneDistance, farPlaneDistance)
                    : Matrix.CreateOrthographic(viewPort.Width, viewPort.Height, nearPlaneDistance, farPlaneDistance);
                return projectionMatrix;
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Create a camera and say what the viewport covers!
        /// </summary>
        /// <param name="fieldOfView"></param>
        /// <param name="aspectRatio"></param>
        /// <param name="nearPlaneDistance"></param>
        /// <param name="farPlaneDistance"></param>
        /// <param name="viewPort"></param>
        public Camera(float fieldOfView, float aspectRatio,
           float nearPlaneDistance, float farPlaneDistance, Viewport viewPort)
            : this(fieldOfView, aspectRatio, nearPlaneDistance, farPlaneDistance,
                  CameraProjectionType.Perspective, viewPort)
        {
        }

        /// <summary>
        /// Create a camera and control everything!
        /// </summary>
        /// <param name="fieldOfView"></param>
        /// <param name="aspectRatio"></param>
        /// <param name="nearPlaneDistance"></param>
        /// <param name="farPlaneDistance"></param>
        /// <param name="projectionType"></param>
        /// <param name="viewPort"></param>
        public Camera(float fieldOfView, float aspectRatio,
        float nearPlaneDistance, float farPlaneDistance,
        CameraProjectionType projectionType,
        Viewport viewPort)
        {
            FieldOfView = fieldOfView;
            AspectRatio = aspectRatio;
            NearPlaneDistance = nearPlaneDistance;
            FarPlaneDistance = farPlaneDistance;
            this.projectionType = projectionType;
            this.viewPort = viewPort;
        }

        #endregion Constructors

        /// <summary>
        /// Used to set either a perspective or orthographic projection on the camera in the scene
        /// </summary>
        /// <see cref="GDLibrary.Components.Camera"/>
        public enum CameraProjectionType : sbyte
        {
            Perspective, Orthographic
        }
    }
}