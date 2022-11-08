using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    public class Material
    {
        #region Fields

        private Texture2D diffuse;
        private float alpha;

        #endregion Fields

        #region Properties

        public Texture2D Diffuse { get => diffuse; protected set => diffuse = value; }
        public float Alpha { get => alpha; protected set => alpha = value; }

        #endregion Properties

        #region Constructors

        public Material(Texture2D diffuse, float alpha)
        {
            this.diffuse = diffuse;
            this.alpha = alpha;
        }

        #endregion Constructors
    }
}