using System.Collections.Generic;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace GD.Engine
{
    /// <summary>
    /// Orchestrates the drawing/rendering of an object (formerly SpriteRenderer)
    /// </summary>
    public class Renderer2D : Component
    {
        #region Fields

        private Material2D material;  //textures, alpha

        #endregion Fields

        #region Properties

        public Material2D Material { get => material; set => material = value; }

        #endregion Properties

        #region Constructors

        public Renderer2D(Material2D material)
        {
            this.material = material;
        }

        #endregion Constructors

        #region Actions - Draw

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            material.Draw(spriteBatch, transform);
        }

        #endregion Actions - Draw
    }
}