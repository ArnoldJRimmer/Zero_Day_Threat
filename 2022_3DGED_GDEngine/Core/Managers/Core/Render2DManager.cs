using GD.Engine.Events;
using GD.Engine.Globals;
using GD.Engine.Managers;
using Microsoft.Xna.Framework;
using System.Windows.Forms;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace GD.Engine
{
    public class Render2DManager : PausableDrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private UserInterfaceManager userInterfaceManager;

        public Render2DManager(Game game, SpriteBatch spriteBatch, UserInterfaceManager userInterfaceManager)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.userInterfaceManager = userInterfaceManager;
        }

        protected override void HandleEvent(EventData eventData)
        {
            if (eventData.EventCategoryType == EventCategoryType.Menu)
            {
                if (eventData.EventActionType == EventActionType.OnPlay)
                    StatusType = StatusType.Off;
                else if (eventData.EventActionType == EventActionType.OnPause)
                    StatusType = StatusType.Updated | StatusType.Drawn;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (IsDrawn)
            {
                spriteBatch.Begin();
                foreach (GameObject gameObject in userInterfaceManager.ActiveScene.ObjectList)
                {
                    gameObject.GetComponent<SpriteRenderer>().Draw(spriteBatch);
                }
                spriteBatch.End();
            }
        }
    }
}