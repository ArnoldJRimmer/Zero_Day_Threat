using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GD.Engine.Managers
{
    /// <summary>
    /// Stores all cameras and updates the active camera
    /// </summary>
    public class CameraManager
    {
        #region Fields

        private Camera activeCamera = null;
        private GameObject activeGameObject;
        private Dictionary<string, GameObject> cameras;

        #endregion Fields

        #region Properties

        public string ActiveCameraName
        {
            get
            {
                if (activeGameObject == null)
                    throw new NullReferenceException("ActiveCamera not set! Call SetActiveCamera()");

                return activeGameObject.Name;
            }
        }

        public Camera ActiveCamera
        {
            get
            {
                if (activeCamera == null)
                    throw new NullReferenceException("ActiveCamera not set! Call SetActiveCamera()");

                return activeCamera;
            }
        }

        #endregion Properties

        #region Constructors

        public CameraManager()
        {
            cameras = new Dictionary<string, GameObject>();
        }

        #endregion Constructors

        #region Actions - Add, SetActiveCamera

        public bool Add(string id, GameObject camera)
        {
            id = id.Trim().ToLower();

            if (cameras.ContainsKey(id))
                return false;

            cameras.Add(id, camera);
            return true;
        }

        public Camera SetActiveCamera(string id)
        {
            GameObject cameraGameObject = null;

            id = id.Trim().ToLower();

            if (cameras.ContainsKey(id))
                cameraGameObject = cameras[id];

            if (cameraGameObject != null)
            {
                activeCamera = cameraGameObject.GetComponent<Camera>();
                activeGameObject = cameraGameObject;
            }

            return activeCamera;
        }

        #endregion Actions - Add, SetActiveCamera

        #region Actions - Update

        public virtual void Update(GameTime gameTime)
        {
            activeGameObject.Update(gameTime);
        }

        #endregion Actions - Update
    }
}