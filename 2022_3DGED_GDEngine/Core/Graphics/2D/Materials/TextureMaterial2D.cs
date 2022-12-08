using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text;

namespace GD.Engine
{
    /// <summary>
    /// Stores the texture for a 2D drawn object (formerly SpriteMaterial)
    /// </summary>
    /// <see cref="Renderer2D"/>
    public class TextureMaterial2D : Material2D
    {
        #region Fields

        protected Texture2D texture;
        protected Rectangle sourceRectangle;
        protected Rectangle originalSourceRectangle;

        #endregion Fields

        #region Properties

        public Texture2D Texture { get => texture; set => texture = value; }
        public Rectangle SourceRectangle { get => sourceRectangle; set => sourceRectangle = value; }
        public int SourceRectangleWidth { get => sourceRectangle.Width; set => sourceRectangle.Width = value; }
        public int SourceRectangleHeight { get => sourceRectangle.Height; set => sourceRectangle.Height = value; }

        public Rectangle OriginalSourceRectangle
        {
            get
            {
                return originalSourceRectangle;
            }
        }

        #endregion Properties

        #region Constructors

        public TextureMaterial2D(Texture2D texture, Color color, float layerDepth, Vector2 origin)
             : this(texture, color, layerDepth, origin, SpriteEffects.None, new Rectangle(0, 0, texture.Width, texture.Height))
        {
        }

        public TextureMaterial2D(Texture2D texture, Color color, float layerDepth)
            : this(texture, color, layerDepth, Vector2.Zero, SpriteEffects.None, new Rectangle(0, 0, texture.Width, texture.Height))
        {
        }

        public TextureMaterial2D(Texture2D texture, Color color)
            : this(texture, color, 0, Vector2.Zero, SpriteEffects.None, new Rectangle(0, 0, texture.Width, texture.Height))
        {
        }

        public TextureMaterial2D(Texture2D texture, Color color, float layerDepth, Vector2 origin, SpriteEffects spriteEffects, Rectangle sourceRectangle)
            : base(color, layerDepth, origin, spriteEffects)
        {
            this.texture = texture;
            this.sourceRectangle = originalSourceRectangle = sourceRectangle;

            //store dimensions for any collider2D bounding box
            unscaledDimensions = new Vector2(texture.Width, texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch, Transform transform)
        {
            if (spriteBatch == null || transform == null)
                throw new ArgumentNullException("One or more args == null!");

            spriteBatch?.Draw(texture,
                transform.Translation.To2D(),
                sourceRectangle,
                color,
                transform.Rotation.Z,             //TODO - remember this is degrees!
                origin,
                transform.Scale.To2D(),
                spriteEffects,
                layerDepth);
        }

        #endregion Constructors
    }
}