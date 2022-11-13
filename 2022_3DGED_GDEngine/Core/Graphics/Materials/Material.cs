using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    public class Material
    {
        #region Fields

        private Color diffuseColor;
        private Texture2D diffuse;
        private float alpha;

        #endregion Fields

        #region Properties

        public Texture2D Diffuse { get => diffuse; protected set => diffuse = value; }
        public float Alpha { get => alpha; protected set => alpha = value; }
        public Color DiffuseColor { get => diffuseColor; protected set => diffuseColor = value; }

        #endregion Properties

        #region Constructors

        public Material(Texture2D diffuse, float alpha, Color color)
        {
            this.diffuse = diffuse;
            this.alpha = alpha;
            this.diffuseColor = color;
        }

        public Material(Texture2D diffuse, float alpha)
            : this(diffuse, alpha, Color.White)
        {
        }

        #endregion Constructors
    }
}