using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace GD.Engine
{
    /// <summary>
    /// Stores the surface properties for a 2D drawn object
    /// </summary>
    /// <see cref="Renderer2D"/>
    public abstract class Material2D
    {
        #region Fields

        protected Color color;
        protected float layerDepth;
        protected Vector2 origin;
        protected SpriteEffects spriteEffects;
        protected Vector2 unscaledDimensions;

        #endregion Fields

        #region Properties

        public Color Color { get => color; set => color = value; }

        public float LayerDepth
        {
            get => layerDepth;
            set => layerDepth = value >= 0 && value <= 1 ? value : 0;
        }

        public Vector2 Origin { get => origin; set => origin = value; }
        public SpriteEffects SpriteEffects { get => spriteEffects; set => spriteEffects = value; }
        public Vector2 UnscaledDimensions { get => unscaledDimensions; set => unscaledDimensions = value; }

        #endregion Properties

        #region Constructors

        public Material2D(Color color, float layerDepth, Vector2 origin, SpriteEffects spriteEffects)
        {
            this.color = color;
            LayerDepth = layerDepth;
            Origin = origin;
            SpriteEffects = spriteEffects;
        }

        public abstract void Draw(SpriteBatch spriteBatch, Transform transform);

        #endregion Constructors
    }
}