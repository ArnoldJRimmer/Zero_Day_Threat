using Microsoft.Xna.Framework;

namespace GD.Engine
{
    public interface IEffect
    {
        //set World
        public void SetWorld(Matrix world);

        //set View and Projection
        public void SetCamera(Camera camera);

        //set surface appearance
        public void SetMaterial(Material material);

        //apply to shader
        public void Apply();
    }
}