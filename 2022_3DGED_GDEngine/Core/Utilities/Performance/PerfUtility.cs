using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GD.Engine.Utilities
{
    public class PerfUtility : PausableDrawableGameComponent
    {
        #region Statics

        private static readonly int MAX_TIME_BETWEEN_FPS_UPDATES_IN_MS = 1000;

        #endregion Statics

        #region Fields

        private SpriteBatch spriteBatch;

        /// <summary>
        /// Starting position on UI for first line of performance info
        /// </summary>
        private Vector2 textStartPosition;

        /// <summary>
        /// Offset/separation between each line of performance info
        /// </summary>
        private Vector2 textOffset;

        /// <summary>
        /// Stores a list of Draw function that add string content to the output window
        /// </summary>
        public List<SpriteBatchInfo> infoList;

        /// <summary>
        /// Time since last update to performance UI in MS
        /// </summary>
        private float totalTimeSinceLastFPSUpdate;

        /// <summary>
        /// FPS value shown in the performance UI
        /// </summary>
        private int fps;

        /// <summary>
        /// FPS value in the Update() when MAX_TIME_BETWEEN_FPS_UPDATES_IN_MS has elapsed
        /// </summary>
        private int fpsCountSinceLastRefresh;

        #endregion Fields

        #region Constructors

        public PerfUtility(Game game, SpriteBatch spriteBatch, Vector2 textStartPosition, Vector2 textOffset)
        : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.textStartPosition = textStartPosition;
            this.textOffset = textOffset;

            infoList = new List<SpriteBatchInfo>();
        }

        #endregion Constructors

        #region Update & Draw

        public override void Update(GameTime gameTime)
        {
            if (StatusType != StatusType.Off)
            {
                //accumulate time until next text update
                totalTimeSinceLastFPSUpdate += gameTime.ElapsedGameTime.Milliseconds;

                //count the frames
                fpsCountSinceLastRefresh++;

                //every 500ms send the 2x frame count to fpsCountToShow
                if (totalTimeSinceLastFPSUpdate >= MAX_TIME_BETWEEN_FPS_UPDATES_IN_MS)
                {
                    //reset time until next count update
                    totalTimeSinceLastFPSUpdate = 0;

                    //store value to show in Draw()
                    fps = fpsCountSinceLastRefresh;

                    //reset frame count
                    fpsCountSinceLastRefresh = 0;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (StatusType != StatusType.Off)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null);

                //draw all SpriteBatchInfo added to the list
                for (int i = 0; i < infoList.Count; i++)
                {
                    if (infoList[i] is FPSInfo)
                        infoList[i].Draw(fps.ToString(), textStartPosition + i * textOffset);
                    else
                        infoList[i].Draw(textStartPosition + i * textOffset);
                }

                spriteBatch.End();
            }
        }

        //OLD
        //public override void Draw(GameTime gameTime)
        //{
        //    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null);
        //    spriteBatch.DrawString(spriteFont, $"FPS [{fps}]", fpsTextStartPosition, fpsTextColor);

        //    //name
        //    spriteBatch.DrawString(spriteFont, $"Name:{Application.CameraManager.ActiveCameraName}", fpsTextStartPosition + new Vector2(0, 20), Color.Yellow);

        //    //position
        //    var camPos = Application.CameraManager.ActiveCamera.transform.translation;
        //    camPos.Round(1);
        //    spriteBatch.DrawString(spriteFont, $"Pos:{camPos}", fpsTextStartPosition + new Vector2(0, 40), Color.Yellow);

        //    //rotation
        //    var camRot = Application.CameraManager.ActiveCamera.transform.rotation;
        //    camRot.Round(1);
        //    spriteBatch.DrawString(spriteFont, $"Rot:{camRot}", fpsTextStartPosition + new Vector2(0, 60), Color.Yellow);

        //    var camScale = Application.CameraManager.ActiveCamera.transform.scale;
        //    camScale.Round(1);
        //    spriteBatch.DrawString(spriteFont, $"Scale:{camScale}", fpsTextStartPosition + new Vector2(0, 80), Color.Yellow);

        //    //TODO - add more here
        //    spriteBatch.End();
        //}

        #endregion Update & Draw
    }
}