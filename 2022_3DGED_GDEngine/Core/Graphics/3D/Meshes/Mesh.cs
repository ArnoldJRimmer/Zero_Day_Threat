using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    /// <summary>
    /// Stores the vertices and indices and creates the vertexbuffer and indexbuffer for a mesh
    /// </summary>
    public abstract class Mesh<T> where T : struct, IVertexType
    {
        #region Fields

        protected T[] vertices;
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

        /// <summary>
        /// Override to specify the array of vertices and indices - see child classes
        /// </summary>
        protected virtual void CreateGeometry()
        {
        }

        /// <summary>
        /// Reserve space on VRAM and move the vertex and index data to VRAM before first Draw()
        /// </summary>
        /// <param name="graphicsDevice"></param>
        protected virtual void CreateBuffers(GraphicsDevice graphicsDevice)
        {
            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(T), vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            indexBuffer = new IndexBuffer(graphicsDevice, typeof(ushort), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        /// <summary>
        /// Called to draw the mesh - see overridden methods in child classes
        /// </summary>
        /// <param name="graphicsDevice"></param>
        public virtual void Draw(GraphicsDevice graphicsDevice,
            IEffect effect, Transform transform, Camera camera,
            Material material)
        {
            //effect.SetWorld(transform.World);
            //effect.SetCamera(camera);
            //effect.SetMaterial(material);

            ////apply all settings
            //effect.Apply();

            //graphicsDevice.SetVertexBuffer(vertexBuffer);
            //graphicsDevice.Indices = indexBuffer;
            //graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, indexBuffer.IndexCount / 3);
        }

        #endregion Actions - Initialize, CreateGeometry, CreateBuffers, Draw
    }
}