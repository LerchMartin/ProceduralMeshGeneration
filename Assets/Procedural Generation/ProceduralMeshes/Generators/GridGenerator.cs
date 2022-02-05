using ProceduralMeshes.Streams;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;

namespace ProceduralMeshes.Generators
{
    public struct Grid : IMeshGenerator
    {
        public int Resolution { get; set; }

        public int VertexCount => 4 * Resolution * Resolution;
        public int IndexCount => 6 * Resolution * Resolution;

        public Bounds Bounds => new Bounds(new Vector3(0.5f, 0.5f), new Vector3(1f, 1f));

        public int JobLength => Resolution * Resolution;


        public void Execute<S>(int i, S streams) where S : struct, IMeshStream
        {
            int verticeIndex = 4 * i;
            int triangleIndex = 2 * i;

            int y = i / Resolution;
            int x = i - Resolution * y;

            var coordinates = new float4(x, x + .9f, y, y + .9f);

            half halfZero = math.half(0);
            half halfOne = math.half(1);
            half halfMinusOne = math.half(-1);

            var vertex = new Vertex();
            vertex.normal.z = -1f;
            vertex.tangent.xw = math.half2(halfOne, halfMinusOne);

            vertex.position.xy = coordinates.xz;
            streams.SetVertex(verticeIndex + 0, vertex);

            vertex.position.xy = coordinates.yz;
            vertex.texCoord0 = new half2(halfOne, halfZero);
            streams.SetVertex(verticeIndex + 1, vertex);

            vertex.position.xy = coordinates.xw;
            vertex.texCoord0 = new half2(halfZero, halfOne);
            streams.SetVertex(verticeIndex + 2, vertex);

            vertex.position.xy = coordinates.yw;
            vertex.texCoord0 = new half2(halfOne, halfOne);
            streams.SetVertex(verticeIndex + 3, vertex);

            streams.SetTriangle(triangleIndex + 0, verticeIndex + new int3(0, 2, 1));
            streams.SetTriangle(triangleIndex + 1, verticeIndex + new int3(1, 2, 3));
        }
    }
}
