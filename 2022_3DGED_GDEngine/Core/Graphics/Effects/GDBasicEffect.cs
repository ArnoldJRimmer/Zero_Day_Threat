using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using Effect = Microsoft.Xna.Framework.Graphics.Effect;

namespace GD.Engine
{
    /// <summary>
    /// Wrapper class for a BasicEffect to integrate its use into the engine
    /// </summary>
    public class GDBasicEffect : IEffect
    {
        #region Fields

        private BasicEffect effect;

        #endregion Fields

        #region Constructors

        public GDBasicEffect(Effect effect)
        {
            this.effect = effect as BasicEffect;
        }

        #endregion Constructors

        #region Actions - SetWorld, Apply etc

        public void SetWorld(Matrix world)
        {
            effect.World = world;
        }

        public void SetCamera(Camera camera)
        {
            effect.View = camera.View;
            effect.Projection = camera.Projection;
        }

        public void SetMaterial(Material material)
        {
            //set color
            effect.DiffuseColor = material.DiffuseColor.ToVector3();
            //set texture
            effect.Texture = material.Diffuse;
            //set transparency
            effect.Alpha = material.Alpha;
        }

        public void Apply()
        {
            effect.CurrentTechnique.Passes[0].Apply();
        }

        #endregion Actions - SetWorld, Apply etc
    }
}