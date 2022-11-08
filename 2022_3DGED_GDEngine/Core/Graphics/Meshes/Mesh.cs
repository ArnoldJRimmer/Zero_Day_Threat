using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    /// <summary>
    /// Stores the vertices and indices and creates the vertexbuffer and indexbuffer for a mesh
    /// </summary>
    public abstract class Mesh
    {
        #region Fields

        protected VertexPositionNormalTexture[] vertices;
        protected ushort[] indices;
        protected VertexBuffer vertexBuffer;
        protected IndexBuffer indexBuffer;
        protected GraphicsDevice graphicsDevice;

        #endregion Fields

        #region Constructors

        public Mesh(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        #endregion Constructors

        #region Actions - Initialize, CreateGeometry, CreateBuffers, Draw

        protected virtual void Initialize()
        {
            //set up the position, normal, texture UVs etc
            CreateGeometry();

            //set up the buffers on VRAM with the vertex array and index array
            CreateBuffers(graphicsDevice);
        }

        protected abstract void CreateGeometry();

        /// <summary>
        /// Reserve space on VRAM and move the vertex and index data to VRAM before first Draw()
        /// </summary>
        /// <param name="graphicsDevice"></param>
        protected virtual void CreateBuffers(GraphicsDevice graphicsDevice)
        {
            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormalTexture), vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            indexBuffer = new IndexBuffer(graphicsDevice, typeof(ushort), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        /// <summary>
        /// Called to draw the mesh
        /// </summary>
        /// <param name="graphicsDevice"></param>
        public virtual void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;
            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, indexBuffer.IndexCount / 3);
        }

        #endregion Actions - Initialize, CreateGeometry, CreateBuffers, Draw
    }
}