using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GD.Engine
{
    public class TextSpriteMaterial : SpriteMaterial
    {
        private SpriteFont spriteFont;
        private StringBuilder stringBuilder;

        public SpriteFont SpriteFont { get => spriteFont; set => spriteFont = value; }
        public StringBuilder StringBuilder { get => stringBuilder; set => stringBuilder = value; }

        public TextSpriteMaterial(Texture2D diffuse, Color color, SpriteFont spriteFont, StringBuilder stringBuilder)
            : base(diffuse, color)
        {
            this.spriteFont = spriteFont;
            this.stringBuilder = stringBuilder;
        }
    }
}