using Microsoft.Xna.Framework;

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

        #endregion Fields

        #region Properties

        public Matrix World //ISRoT
        {
            get
            {
                //TODO - improve so not always calculated for when object does not move
                return Matrix.Identity * Matrix.CreateScale(scale)
                    * Orientation * Matrix.CreateTranslation(translation);
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