using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    public class WireframeMesh<T> : Mesh<T> where T : struct, IVertexType
    {
        public WireframeMesh(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
        }

        /// <summary>
        /// Called to draw the mesh
        /// </summary>
        /// <param name="graphicsDevice"></param>
        public override void Draw(GraphicsDevice graphicsDevice, IEffect effect, Transform transform, Camera camera, Material material)
        {
            effect.SetWorld(transform.World);
            effect.SetCamera(camera);
            effect.SetMaterial(material);

            //apply all settings
            effect.Apply();

            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;
            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, indexBuffer.IndexCount / 2);
        }
    }
}