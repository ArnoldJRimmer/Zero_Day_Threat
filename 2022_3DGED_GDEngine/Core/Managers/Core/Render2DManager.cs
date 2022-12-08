using GD.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace GD.Engine
{
    public class Render2DManager : PausableDrawableGameComponent
    {
        #region Fields

        private SpriteBatch spriteBatch;
        private SceneManager<Scene2D> sceneManager;
        private SamplerState samplerState;

        #endregion Fields

        #region Constructors

        public Render2DManager(Game game, SpriteBatch spriteBatch,
            SceneManager<Scene2D> sceneManager)
     : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.sceneManager = sceneManager;

            //used when drawing textures
            samplerState = new SamplerState();
            samplerState.Filter = TextureFilter.Linear;
        }

        #endregion Constructors

        #region Actions - Draw

        public override void Draw(GameTime gameTime)
        {
            if (IsDrawn)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, samplerState, null, null, null, null);
                foreach (GameObject gameObject in sceneManager.ActiveScene.ObjectList)
                {
                    List<Renderer2D> renderers = gameObject.GetComponents<Renderer2D>();
                    if (renderers != null)
                    {
                        foreach (Renderer2D renderer in renderers)
                            renderer.Draw(spriteBatch);
                    }
                }
                spriteBatch.End();
            }
        }

        #endregion Actions - Draw
    }
}