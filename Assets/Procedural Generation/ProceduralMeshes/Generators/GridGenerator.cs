using ProceduralMeshes.Streams;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ProceduralMeshes.Generators
{
    public struct Grid : IMeshGenerator
    {
        public int VertexCount => 4;

        public int IndexCount => 6;

        public int JobLength => 1;

        public Bounds Bounds => new Bounds(new Vector3(0.5f, 0.5f), new Vector3(1f, 1f));

        public void Execute<S>(int i, S streams) where S : struct, IMeshStream
        {

            half halfZero = math.half(0);
            half halfOne = math.half(1);
            half halfMinusOne = math.half(-1);

            var vertex = new Vertex();
            vertex.normal.z = -1f;
            vertex.tangent.xw = math.half2(halfOne, halfMinusOne);

            vertex.position = new float3(1, 0, 0);
            vertex.texCoord0 = new half2(halfOne, halfZero);
            streams.SetVertex(1, vertex);

            vertex.position = new float3(0, 1, 0);
            vertex.texCoord0 = new half2(halfZero, halfOne);
            streams.SetVertex(2, vertex);

            vertex.position = new float3(1f, 1f, 0f);
            vertex.texCoord0 = new half2(halfOne, halfOne);
            streams.SetVertex(3, vertex);

            streams.SetTriangle(0, new int3(0, 2, 1));
            streams.SetTriangle(1, new int3(1, 2, 3));
        }
    }
}
