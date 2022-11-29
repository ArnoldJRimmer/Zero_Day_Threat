using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    public class ModelMesh : TexturedMesh<VertexPositionNormalTexture>
    {
        private Model model;

        /// <summary>
        /// Stores bone transforms for the model (e.g. each mesh will normally have one bone)
        /// </summary>
        protected Matrix[] boneTransforms;

        public ModelMesh(GraphicsDevice graphicsDevice, Model model)
            : base(graphicsDevice)
        {
            this.model = model;
            this.boneTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneTransforms);
        }

        public override void Draw(GraphicsDevice graphicsDevice,
            IEffect effect, Transform transform, Camera camera, Material material)
        {
            foreach (Microsoft.Xna.Framework.Graphics.ModelMesh mesh in model.Meshes)
            {
                effect.SetWorld(boneTransforms[mesh.ParentBone.Index] * transform.World);
                effect.SetCamera(camera);
                effect.SetMaterial(material);
                effect.Apply();

                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    graphicsDevice.SetVertexBuffer(meshPart.VertexBuffer);
                    graphicsDevice.Indices = meshPart.IndexBuffer;
                    graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, meshPart.VertexOffset, meshPart.StartIndex, meshPart.PrimitiveCount);
                }
            }
        }

        protected override void CreateGeometry()
        {
        }
    }
}

/*
 using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    public class ModelMesh : Mesh
    {
        private Model model;

        public ModelMesh(GraphicsDevice graphicsDevice, Model model)
            : base(graphicsDevice)
        {
            this.model = model;

            Initialize();
        }

        protected override void CreateGeometry()
        {
            //extracts from the model the vertices and indices
            model.ExtractData(ref graphicsDevice, out vertices, out indices);
        }
    }
}
 */