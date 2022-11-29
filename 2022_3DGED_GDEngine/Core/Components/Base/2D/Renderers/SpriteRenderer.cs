using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace GD.Engine
{
    /// <summary>
    /// Orchestrates the drawing/rendering of an object
    /// </summary>
    public class SpriteRenderer : Component
    {
        private SpriteMaterial material;  //textures, alpha
        private UIElement uiElement;      //text, texture

        public SpriteRenderer(SpriteMaterial material,
            UIElement uiElement)
        {
            this.material = material;
            this.uiElement = uiElement;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            uiElement.Draw(spriteBatch, transform, material);
        }
    }
}