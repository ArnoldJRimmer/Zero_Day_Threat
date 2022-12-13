using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GD.Engine
{
    public class UITextElement : UIElement
    {
        public void Draw(SpriteBatch spriteBatch,
          Transform transform, SpriteMaterial material)
        {
            if (spriteBatch == null || transform == null || material == null)
                throw new ArgumentNullException("One or more args == null!");

            var translation = transform.Translation;
            var scale = transform.Scale;

            var textSpriteMaterial = material as TextSpriteMaterial;

            spriteBatch.DrawString(textSpriteMaterial.SpriteFont,
                textSpriteMaterial.StringBuilder,
                translation.To2D(),
                material.DiffuseColor,
                //TODO - remember this is degrees!
                transform.Rotation.Z,
                material.Origin,
                scale.To2D(),
                material.SpriteEffects,
                material.LayerDepth);
        }
    }

    public class UITextureElement : UIElement
    {
        public void Draw(SpriteBatch spriteBatch,
            Transform transform, SpriteMaterial material)
        {
            if (spriteBatch == null || transform == null || material == null)
                throw new ArgumentNullException("One or more args == null!");

            var translation = transform.Translation;
            var scale = transform.Scale;

            spriteBatch?.Draw(material.Diffuse,
                translation.To2D(),
                material.SourceRectangle,
                material.DiffuseColor,
                //TODO - remember this is degrees!
                transform.Rotation.Z,
                material.Origin,
                scale.To2D(),
                material.SpriteEffects,
                material.LayerDepth);
        }
    }

    public interface UIElement
    {
        //no common state information?
        public void Draw(SpriteBatch spriteBatch,
            Transform transform,
            SpriteMaterial material);
    }
}