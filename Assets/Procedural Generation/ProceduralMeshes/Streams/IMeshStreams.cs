using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ProceduralMeshes.Streams
{
    public interface IMeshStream
    {
        void Initialize(Mesh.MeshData meshData, int vertexCount, int indexCount, Bounds bounds);
        void SetVertex(int index, Vertex data);
        void SetTriangle(int index, int3 triangle);
    }
}
