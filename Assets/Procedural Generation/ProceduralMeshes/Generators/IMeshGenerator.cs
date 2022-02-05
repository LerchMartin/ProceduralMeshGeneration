using ProceduralMeshes.Streams;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralMeshes.Generators
{
    public interface IMeshGenerator
    {
        void Execute<S>(int index, S streams) where S : struct, IMeshStream;
        int Resolution { get; set; }
        int VertexCount { get; }
        int IndexCount { get; }
        int JobLength { get; }
        Bounds Bounds { get; }
    }
}