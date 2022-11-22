using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace GD.Engine
{
    public class AudioEmitterBehaviour : Component
    {
        private AudioEmitter emitter;

        public override void Update(GameTime gameTime)
        {
            //emitter.Position = transform.translation;
            //emitter.Up = transform.World.Up;
            //emitter.Forward = transform.World.Forward;
            ////emitter.Velocity = transform.Velocity;
            base.Update(gameTime);
        }
    }
}