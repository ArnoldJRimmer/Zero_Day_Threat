using GD.Engine.Globals;
using Microsoft.Xna.Framework;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace GD.Engine
{
    public class Render2DManager : PausableDrawableGameComponent
    {
        private readonly SpriteBatch spriteBatch;

        public Render2DManager(Game game,
            StatusType statusType, SpriteBatch spriteBatch)
            : base(game, statusType)
        {
            this.spriteBatch = spriteBatch;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (GameObject gameObject in Application.UISceneManager.ActiveScene.ObjectList)
            {
                gameObject.GetComponent<SpriteRenderer>().Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}