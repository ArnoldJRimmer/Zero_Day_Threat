using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    public class XYZMesh : WireframeMesh<VertexPositionColor>
    {
        public XYZMesh(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            Initialize();
        }

        protected override void CreateGeometry()
        {
            vertices = new VertexPositionColor[]
            {
                //x-axis
                new VertexPositionColor(-Vector3.UnitX, Color.DarkRed),
                new VertexPositionColor(Vector3.UnitX, Color.DarkRed),

                //y-axis
                new VertexPositionColor(-Vector3.UnitY, Color.DarkGreen),
                new VertexPositionColor(Vector3.UnitY, Color.DarkGreen),

                //z-axis
                new VertexPositionColor(-Vector3.UnitZ, Color.DarkBlue),
                new VertexPositionColor(Vector3.UnitZ, Color.DarkBlue)
        };

            indices = new ushort[]
            {
                0, 1, //x
                2, 3, //y
                4, 5  //z
            };
        }

        public override void Draw(GraphicsDevice graphicsDevice, IEffect effect, Transform transform, Camera camera, Material material)
        {
            base.Draw(graphicsDevice, effect, transform, camera, material);
        }
    }
}