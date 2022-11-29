using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    /// <summary>
    /// Orchestrates the drawing/rendering of an object
    /// </summary>
    public class Renderer : Component
    {
        #region Fields

        private IEffect effect; //effect
        private Material material;  //textures, alpha
        private Mesh<VertexPositionNormalTexture> mesh;  //vertices and indices

        #endregion Fields

        #region Properties

        public IEffect Effect { get => effect; set => effect = value; }
        public Material Material { get => material; set => material = value; }
        public Mesh<VertexPositionNormalTexture> Mesh { get => mesh; set => mesh = value; }

        #endregion Properties

        #region Constructors

        public Renderer(IEffect effect, Material material,
            Mesh<VertexPositionNormalTexture> mesh)
        {
            Effect = effect;
            Material = material;
            Mesh = mesh;
        }

        #endregion Constructors

        #region Actions - Draw

        public virtual void Draw(GraphicsDevice graphicsDevice,
            Camera camera)
        {
            //draw the object
            mesh.Draw(graphicsDevice, effect, transform,
                camera, material);
        }

        #endregion Actions - Draw
    }
}