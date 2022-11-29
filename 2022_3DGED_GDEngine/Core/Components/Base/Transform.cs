using Microsoft.Xna.Framework;
using System;

namespace GD.Engine
{
    /// <summary>
    /// Store and manage transform operations e.g. translation, rotation and scale
    /// </summary>
    public class Transform : Component, ICloneable
    {
        #region Events

        public event Action PropertyChanged = null;

        #endregion Events

        #region Fields

        /// <summary>
        /// Used to render any visible game object (i.e. with MeshRenderer or ModelRenderer)
        /// </summary>
        private Matrix worldMatrix;

        /// <summary>
        /// Used to calculate the World matrix and also to calculate the target for a Camera
        /// </summary>
        private Matrix rotationMatrix;

        private Vector3 scale;
        private Vector3 rotation;
        private Vector3 translation;

        /// <summary>
        /// Set to true if the translation, rotation, or scale change which affect World matrix
        /// </summary>
        private bool isWorldDirty = true;

        /// <summary>
        /// Set to true if the rotation changes
        /// </summary>
        private bool isRotationDirty = true;

        #endregion Fields

        #region Properties

        public Vector3 Scale
        {
            get
            {
                return scale;
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return rotation;
            }
        }

        public Vector3 Translation
        {
            get
            {
                return translation;
            }
        }

        public Matrix RotationMatrix
        {
            get
            {
                if (isRotationDirty)
                    UpdateRotationMatrix();

                return rotationMatrix;
            }
        }

        public Matrix World
        {
            get
            {
                if (isWorldDirty || isRotationDirty)
                    UpdateWorldMatrix();

                return worldMatrix;
            }
            set
            {
                worldMatrix = value;
            }
        }

        #endregion Properties

        #region Constructors

        public Transform(Vector3? scale, Vector3? rotation, Vector3? translation)
        {
            this.scale = scale.HasValue ? scale.Value : Vector3.One;
            this.rotation = rotation.HasValue ? rotation.Value : Vector3.Zero;
            this.translation = translation.HasValue ? translation.Value : Vector3.Zero;

            worldMatrix = Matrix.Identity;
            rotationMatrix = Matrix.Identity;

            isWorldDirty = true;
            isRotationDirty = true;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <see cref="GameObject()"/>
        public Transform() : this(null, null, null)
        {
        }

        #endregion Constructors

        #region Actions - Modify Scale, Rotation, Translation

        /// <summary>
        /// Increases/decreases scale by adding/removing individual nullable x,y,z values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void ScaleBy(float? x, float? y, float? z)
        {
            scale.Add(x, y, z);
            isWorldDirty = true;
        }

        /// <summary>
        /// Increases/decreases scale by adding/removing Vector3 passed by reference
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void ScaleBy(ref Vector3 delta)
        {
            scale.Add(ref delta);
            isWorldDirty = true;
        }

        /// <summary>
        /// Increases/decreases scale by adding/removing Vector3 passed by value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void ScaleBy(Vector3 delta)
        {
            ScaleBy(ref delta);
        }

        /// <summary>
        /// Increases/decreases rotation by adding/removing individual nullable x,y,z values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Rotate(float? x, float? y, float? z)
        {
            rotation.Add(x, y, z);
            isWorldDirty = true;
            isRotationDirty = true;
            PropertyChanged?.Invoke();
        }

        /// <summary>
        /// Increases/decreases rotation by adding/removing Vector3 passed by reference
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Rotate(ref Vector3 delta)
        {
            rotation.Add(ref delta);
            isWorldDirty = true;
            isRotationDirty = true;
            PropertyChanged?.Invoke();
        }

        /// <summary>
        /// Increases/decreases rotation by adding/removing Vector3 passed by value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Rotate(Vector3 delta)
        {
            Rotate(ref delta);
        }

        /// <summary>
        /// Increases/decreases translation by adding/removing individual nullable x,y,z values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Translate(float? x, float? y, float? z)
        {
            translation.Add(x, y, z);
            isWorldDirty = true;
            PropertyChanged?.Invoke();
        }

        /// <summary>
        /// Increases/decreases translation by adding/removing Vector3 passed by reference
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Translate(ref Vector3 delta)
        {
            translation.Add(ref delta);
            isWorldDirty = true;
            PropertyChanged?.Invoke();
        }

        /// <summary>
        /// Increases/decreases translation by adding/removing Vector3 passed by value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Translate(Vector3 delta)
        {
            Translate(ref delta);
        }

        /// <summary>
        /// Sets scale using individual nullable x,y,z values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetScale(float? x, float? y, float? z)
        {
            scale.Set(x, y, z);
            isWorldDirty = true;
        }

        /// <summary>
        /// Sets scale using Vector3 pass by reference
        /// </summary>
        /// <param name="newScale"></param>
        public void SetScale(ref Vector3 newScale)
        {
            scale.Set(ref newScale);
            isWorldDirty = true;
        }

        /// <summary>
        /// Sets scale using Vector3 pass by value
        /// </summary>
        /// <param name="newScale"></param>
        public void SetScale(Vector3 newScale)
        {
            SetScale(ref newScale);
        }

        /// <summary>
        /// Sets rotation using individual nullable x,y,z values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetRotation(float? x, float? y, float? z)
        {
            rotation.Set(x, y, z);
            isWorldDirty = true;
            isRotationDirty = true;
            PropertyChanged?.Invoke();
        }

        /// <summary>
        /// Sets rotation using Vector3 pass by reference
        /// </summary>
        /// <param name="newRotation"></param>
        public void SetRotation(ref Vector3 newRotation)
        {
            rotation.Set(ref newRotation);
            isWorldDirty = true;
            isRotationDirty = true;
            PropertyChanged?.Invoke();
        }

        /// <summary>
        /// Sets rotation using Vector3 pass by value
        /// </summary>
        /// <param name="newRotation"></param>
        public void SetRotation(Vector3 newRotation)
        {
            SetRotation(ref newRotation);
        }

        /// <summary>
        /// Sets rotation using Matrix pass by reference
        /// </summary>
        /// <param name="newRotation"></param>
        public void SetRotation(ref Matrix matrix)
        {
            var rotation = Quaternion.CreateFromRotationMatrix(matrix).ToEuler();
            this.rotation.Set(rotation.X, rotation.Y, rotation.Z);
            isWorldDirty = true;
            isRotationDirty = true;
            PropertyChanged?.Invoke();
        }

        /// <summary>
        /// Sets rotation using Matrix pass by value
        /// </summary>
        /// <param name="newRotation"></param>
        public void SetRotation(Matrix matrix)
        {
            SetRotation(ref matrix);
        }

        /// <summary>
        /// Sets translation using individual nullable x,y,z values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetTranslation(float? x, float? y, float? z)
        {
            translation.Set(x, y, z);
            isWorldDirty = true;
            PropertyChanged?.Invoke();
        }

        /// <summary>
        /// Sets translation using Vector3 pass by reference
        /// </summary>
        /// <param name="newTranslation"></param>
        public void SetTranslation(ref Vector3 newTranslation)
        {
            translation.Set(ref newTranslation);
            isWorldDirty = true;
            PropertyChanged?.Invoke();
        }

        /// <summary>
        /// Sets translation using Vector3 pass by value
        /// </summary>
        /// <param name="newTranslation"></param>
        public void SetTranslation(Vector3 newTranslation)
        {
            SetTranslation(ref newTranslation);
        }

        public override void Update(GameTime gameTime)
        {
            if (isRotationDirty)
                UpdateRotationMatrix();

            if (isWorldDirty)
                UpdateWorldMatrix();
        }

        private void UpdateRotationMatrix()
        {
            rotationMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X))
                      * Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y))
                          * Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
            isRotationDirty = false;
        }

        private void UpdateWorldMatrix()
        {
            worldMatrix = Matrix.Identity
                      * Matrix.CreateScale(scale)
                          * Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X))
                      * Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y))
                          * Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z))
                          * Matrix.CreateTranslation(translation);
            isWorldDirty = false;
        }

        #endregion Actions - Modify Scale, Rotation, Translation

        #region Actions - Housekeeping

        //TODO - Dispose

        /// <summary>
        /// Deep or shallow, or mixed?
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            //value types - deep
            //reference types - shallow
            return MemberwiseClone();
        }

        #endregion Actions - Housekeeping
    }
}

/*
namespace GD.Engine
{
    /// <summary>
    /// Store and manage transform values (position, rotation, scale)
    /// </summary>
    public class Transform : Component
    {
        #region Fields

        /// <summary>
        /// Scale relative to the parent transform
        /// </summary>
        public Vector3 scale;

        /// <summary>
        /// Rotation relative to the parent transform
        /// </summary>
        public Vector3 rotation;

        /// <summary>
        /// Translation relative to the parent transform
        /// </summary>
        public Vector3 translation;

        private Matrix world;

        #endregion Fields

        #region Properties

        public Matrix World //ISRoT
        {
            get
            {
                //TODO - improve so not always calculated for when object does not move
                world = Matrix.Identity * Matrix.CreateScale(scale)
                    * Orientation * Matrix.CreateTranslation(translation);

                return world;
            }
            set
            {
                world = value;
            }
        }

        public Matrix Orientation
        {
            get
            {
                //TODO - improve so not always calculated for when object does not rotate
                return Matrix.CreateRotationX(rotation.X)
                    * Matrix.CreateRotationY(rotation.Y)
                    * Matrix.CreateRotationZ(rotation.Z);
            }
        }

        #endregion Properties

        #region Constructors

        public Transform(Vector3? scale, Vector3? rotation, Vector3? translation)
        {
            this.scale = scale.HasValue ? scale.Value : Vector3.One;
            this.rotation = rotation.HasValue ? rotation.Value : Vector3.Zero;
            this.translation = translation.HasValue ? translation.Value : Vector3.Zero;
        }

        #endregion Constructors

        #region Actions - Modify Scale, Rotation, Translation

        /// <summary>
        /// Increases/decreases scale by adding/removing individual nullable x,y,z values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Scale(float? x, float? y, float? z)
        {
            scale.Add(x, y, z);
        }

        /// <summary>
        /// Increases/decreases scale by adding/removing Vector3
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Scale(Vector3 delta)
        {
            scale.Add(ref delta);
        }

        /// <summary>
        /// Increases/decreases rotation by adding/removing individual nullable x,y,z values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Rotate(float? x, float? y, float? z)
        {
            rotation.Add(x, y, z);
        }

        /// <summary>
        /// Increases/decreases rotation by adding/removing Vector3
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Rotate(Vector3 delta)
        {
            rotation.Add(delta);
        }

        /// <summary>
        /// Increases/decreases translation by adding/removing individual nullable x,y,z values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Translate(float? x, float? y, float? z)
        {
            translation.Add(x, y, z);
        }

        /// <summary>
        /// Increases/decreases translation by adding/removing Vector3
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Translate(Vector3 delta)
        {
            translation.Add(delta);
        }

        /// <summary>
        /// Sets scale using individual nullable x,y,z values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetScale(float? x, float? y, float? z)
        {
            scale.Set(x, y, z);
        }

        /// <summary>
        /// Sets scale using Vector3
        /// </summary>
        /// <param name="newScale"></param>
        public void SetScale(Vector3 newScale)
        {
            scale.Set(newScale);
        }

        /// <summary>
        /// Sets rotation using individual nullable x,y,z values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetRotation(float? x, float? y, float? z)
        {
            rotation.Set(x, y, z);
        }

        /// <summary>
        /// Sets rotation using Vector3
        /// </summary>
        /// <param name="newRotation"></param>
        public void SetRotation(Vector3 newRotation)
        {
            rotation.Set(newRotation);
        }

        /// <summary>
        /// Sets translation using individual nullable x,y,z values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetTranslation(float? x, float? y, float? z)
        {
            translation.Set(x, y, z);
        }

        /// <summary>
        /// Sets translation using Vector3 pass by value
        /// </summary>
        /// <param name="newTranslation"></param>
        public void SetTranslation(Vector3 newTranslation)
        {
            translation.Set(newTranslation);
        }

        #endregion Actions - Modify Scale, Rotation, Translation

        //TODO - clone etc
    }
}
*/