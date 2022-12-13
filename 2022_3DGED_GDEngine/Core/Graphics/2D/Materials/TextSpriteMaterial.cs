using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Text;

namespace GD.Engine
{
    public class TextMaterial2D : Material2D
    {
        #region Fields

        protected SpriteFont spriteFont;
        protected StringBuilder stringBuilder;
        private Vector2 textOffset;

        #endregion Fields

        #region Constructors

        public SpriteFont SpriteFont { get => spriteFont; set => spriteFont = value; }
        public StringBuilder StringBuilder { get => stringBuilder; set => stringBuilder = value; }
        public Vector2 TextOffset { get => textOffset; set => textOffset = value; }

        #endregion Constructors

        #region Constructors

        public TextMaterial2D(SpriteFont spriteFont, StringBuilder stringBuilder, Vector2 textOffset, Color color)
            : this(spriteFont, stringBuilder, textOffset, color, 0, Vector2.Zero, SpriteEffects.None)
        {
        }

        public TextMaterial2D(SpriteFont spriteFont, string text, Vector2 textOffset, Color color)
            : this(spriteFont, new StringBuilder(text), textOffset, color, 0, Vector2.Zero, SpriteEffects.None)
        {
        }

        public TextMaterial2D(SpriteFont spriteFont, StringBuilder stringBuilder, Vector2 textOffset, Color color, float layerDepth)
          : this(spriteFont, stringBuilder, textOffset, color, layerDepth, Vector2.Zero, SpriteEffects.None)
        {
        }

        public TextMaterial2D(SpriteFont spriteFont, string text, Vector2 textOffset, Color color, float layerDepth)
            : this(spriteFont, new StringBuilder(text), textOffset, color, layerDepth, Vector2.Zero, SpriteEffects.None)
        {
        }

        public TextMaterial2D(SpriteFont spriteFont, string text, Vector2 textOffset, Color color, float layerDepth, Vector2 origin, SpriteEffects spriteEffects)
            : this(spriteFont, new StringBuilder(text), textOffset, color, layerDepth, origin, spriteEffects)
        {
        }

        public TextMaterial2D(SpriteFont spriteFont, StringBuilder stringBuilder, Vector2 textOffset, Color color, float layerDepth, Vector2 origin, SpriteEffects spriteEffects)
            : base(color, layerDepth, origin, spriteEffects)
        {
            this.spriteFont = spriteFont;
            this.stringBuilder = stringBuilder;
            this.textOffset = textOffset;

            //store dimensions for any collider2D bounding box
            unscaledDimensions = spriteFont.MeasureString(stringBuilder.ToString());
        }

        public override void Draw(SpriteBatch spriteBatch, Transform transform)
        {
            if (spriteBatch == null || transform == null)
                throw new ArgumentNullException("One or more args == null!");

            spriteBatch.DrawString(spriteFont,
                stringBuilder,
                transform.Translation.To2D() + textOffset,
                color,
                transform.Rotation.Z, //TODO - remember this is degrees!
                origin,
                transform.Scale.To2D(),
                spriteEffects,
                layerDepth);
        }

        #endregion Constructors
    }
}