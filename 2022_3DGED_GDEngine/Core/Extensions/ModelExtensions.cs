using Microsoft.Xna.Framework.Graphics;
using System;

public static class ModelExtensions
{
    /// <summary>
    /// Extracts vertex and index buffer data from a model
    /// </summary>
    /// <param name="model"></param>
    /// <param name="vertexBuffer"></param>
    /// <param name="indexBuffer"></param>
    /// <see cref="https://www.pluralsight.com/guides/csharp-in-out-ref-parameters"/>
    /// <seealso cref="https://stackoverflow.com/questions/16324050/extracting-vertices-from-fbx-model-using-xna-4-0"/>
    public static void ExtractData<T>(this Model model, ref GraphicsDevice graphicsDevice,
        out VertexBuffer vertexBuffer, out IndexBuffer indexBuffer) where T : struct, IVertexType
    {
        if (model.Meshes.Count != 1)
            throw new NotSupportedException("Model supplied must contain only one mesh!");

        if (model.Meshes[0].MeshParts.Count != 1)
            throw new NotSupportedException("Model supplied must contain a only one mesh with one part!");

        ModelMeshPart part = model.Meshes[0].MeshParts[0];

        if (part == null)
            throw new NullReferenceException("Model contains no part!");

        vertexBuffer = new VertexBuffer(graphicsDevice, typeof(T), part.VertexBuffer.VertexCount, BufferUsage.WriteOnly);
        T[] vertices = new T[part.VertexBuffer.VertexCount];
        part.VertexBuffer.GetData(vertices);
        vertexBuffer.SetData(vertices);

        indexBuffer = new IndexBuffer(graphicsDevice, typeof(ushort), part.IndexBuffer.IndexCount, BufferUsage.WriteOnly);
        ushort[] indices = new ushort[part.IndexBuffer.IndexCount];
        part.IndexBuffer.GetData(indices);
        indexBuffer.SetData(indices);
    }

    public static void ExtractData<T>(this Model model, ref GraphicsDevice graphicsDevice,
        out T[] vertices, out ushort[] indices) where T : struct, IVertexType
    {
        if (model.Meshes.Count != 1)
            throw new NotSupportedException("Model supplied must contain only one mesh!");

        if (model.Meshes[0].MeshParts.Count != 1)
            throw new NotSupportedException("Model supplied must contain a only one mesh with one part!");

        ModelMeshPart part = model.Meshes[0].MeshParts[0];

        if (part == null)
            throw new NullReferenceException("Model contains no part!");

        VertexBuffer vertexBuffer = new VertexBuffer(graphicsDevice, typeof(T), part.VertexBuffer.VertexCount, BufferUsage.WriteOnly);
        vertices = new T[part.VertexBuffer.VertexCount];
        part.VertexBuffer.GetData(vertices);

        IndexBuffer indexBuffer = new IndexBuffer(graphicsDevice, typeof(ushort), part.IndexBuffer.IndexCount, BufferUsage.WriteOnly);
        indices = new ushort[part.IndexBuffer.IndexCount];
        part.IndexBuffer.GetData(indices);
    }
}