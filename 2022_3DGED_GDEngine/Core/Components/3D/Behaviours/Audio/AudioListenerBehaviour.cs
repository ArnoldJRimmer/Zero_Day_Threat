using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace GD.Engine
{
    /// <summary>
    /// Attach to the thing that listens i.e. You - the camera
    /// </summary>
    public class AudioListenerBehaviour : Component
    {
        private AudioListener audioListener;

        public override void Update(GameTime gameTime)
        {
            //audioListener.Position = transform.translation;
            //audioListener.Forward = transform.World.Forward;
            //audioListener.Up = transform.World.Up;
            ////audioListener.Velocity = transform.Velocity;
            base.Update(gameTime);
        }
    }
}