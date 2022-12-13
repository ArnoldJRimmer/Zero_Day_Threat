using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    /// <summary>
    /// Stores the surface properties for a 2D drawn object
    /// </summary>
    /// <see cref="SpriteRenderer"/>
    public class SpriteMaterial : Material
    {
        #region Fields

        private float layerDepth;
        private Vector2 origin;
        private SpriteEffects spriteEffects;

        #endregion Fields

        #region Properties

        public float LayerDepth { get => layerDepth; set => layerDepth = value >= 0 && value <= 1 ? value : 0; }
        public Vector2 Origin { get => origin; set => origin = value; }
        public SpriteEffects SpriteEffects { get => spriteEffects; set => spriteEffects = value; }

        #endregion Properties

        #region Constructors

        public SpriteMaterial(Texture2D diffuse, float alpha, Color color)
        : this(diffuse, alpha, color, 0, Vector2.Zero, SpriteEffects.None)
        {
        }

        public SpriteMaterial(Texture2D diffuse, float alpha, Color color, float layerDepth, Vector2 origin, SpriteEffects spriteEffects)
            : base(diffuse, alpha, color)
        {
            LayerDepth = layerDepth;
            Origin = origin;
            SpriteEffects = spriteEffects;
        }

        #endregion Constructors
    }
}
