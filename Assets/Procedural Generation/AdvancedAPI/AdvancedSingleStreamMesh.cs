using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Rendering;
using Unity.Mathematics;
using System.Runtime.InteropServices;
//using static Unity.Mathematics.math;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class AdvancedSingleStreamMesh : MonoBehaviour
{
    [StructLayout(LayoutKind.Sequential)]
    struct Vertex
    {
        public float3 position;
        public float3 normal;
        public half4 tangent;
        public half2 texCoord0;
    }

    const int vertexAttributeCount = 4;
    // Start is called before the first frame update
    void OnEnable()
    {
        Mesh.MeshDataArray meshDataArray = Mesh.AllocateWritableMeshData(1);
        Mesh.MeshData meshData = meshDataArray[0];

        int vertexCount = 4;
        int triangleIndexCount = 6;

        // Describing vertex attributes

        NativeArray<VertexAttributeDescriptor> vertexAttributes = new NativeArray<VertexAttributeDescriptor>(
            vertexAttributeCount,
            Allocator.Temp,
            NativeArrayOptions.UninitializedMemory
        );

        vertexAttributes[0] = new VertexAttributeDescriptor(VertexAttribute.Position, dimension: 3);
        vertexAttributes[1] = new VertexAttributeDescriptor(VertexAttribute.Normal, dimension: 3);
        vertexAttributes[2] = new VertexAttributeDescriptor(VertexAttribute.Tangent, VertexAttributeFormat.Float16, dimension: 4);
        vertexAttributes[3] = new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float16, dimension: 2);

        // Setting previously descriped vertex attributes
        meshData.SetVertexBufferParams(vertexCount, vertexAttributes);

        NativeArray<Vertex> vertices = meshData.GetVertexData<Vertex>();

        half h0 = math.half(0f), h1 = math.half(1f);

        var vertex = new Vertex
        {
            normal = math.float3(0,0,-1),
            tangent = math.half4(h1, h0, h0, math.half(-1f))
        };

        vertex.position = 0f;
        vertex.texCoord0 = h0;
        vertices[0] = vertex;

        vertex.position = math.float3(1, 0, 0);
        vertex.texCoord0 = math.half2(h1, h0);
        vertices[1] = vertex;

        vertex.position = math.float3(0, 1, 0);
        vertex.texCoord0 = math.half2(h0, h1);
        vertices[2] = vertex;

        vertex.position = math.float3(1f, 1f, 0f);
        vertex.texCoord0 = h1;
        vertices[3] = vertex;

        vertexAttributes.Dispose();

        meshData.SetIndexBufferParams(triangleIndexCount, IndexFormat.UInt16);
        NativeArray<ushort> triangleIndices = meshData.GetIndexData<ushort>();
        triangleIndices[0] = 0;
        triangleIndices[1] = 2;
        triangleIndices[2] = 1;
        triangleIndices[3] = 1;
        triangleIndices[4] = 2;
        triangleIndices[5] = 3;

        meshData.subMeshCount = 1;

        var bounds = new Bounds(new Vector3(0.5f, 0.5f), new Vector3(1f, 1f));

        meshData.SetSubMesh(0, new SubMeshDescriptor(0, triangleIndexCount)
        {
            bounds = bounds,
            vertexCount = vertexCount
        }, MeshUpdateFlags.DontRecalculateBounds);

        triangleIndices.Dispose();

        Mesh mesh = new Mesh
        {
            name = "Procedural Mesh",
            bounds = bounds
        };

        Mesh.ApplyAndDisposeWritableMeshData(meshDataArray, mesh);
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void OnDrawGizmos()
    {

    }
}
