using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    /// <summary>
    /// Orchestrates the drawing/rendering of an object
    /// </summary>
    public class SpriteRenderer : Component
    {
        #region Fields

        private SpriteMaterial material;  //textures, alpha

        #endregion Fields

        #region Properties

        public SpriteMaterial Material { get => material; set => material = value; }

        #endregion Properties

        #region Constructors

        public SpriteRenderer(SpriteMaterial material)
        {
            Material = material;
        }

        #endregion Constructors

        #region Actions - Draw

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //TODO
        }

        #endregion Actions - Draw
    }
}