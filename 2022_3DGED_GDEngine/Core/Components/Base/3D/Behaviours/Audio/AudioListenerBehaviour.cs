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

        public AudioListenerBehaviour()
        {
            audioListener = new AudioListener();
        }

        public override void Update(GameTime gameTime)
        {
            if (transform == null || audioListener == null)
                return;

            audioListener.Position = transform.Translation;
            audioListener.Forward = transform.World.Forward;
            audioListener.Up = transform.World.Up;
            //audioListener.Velocity = transform.Velocity;
        }
    }
}