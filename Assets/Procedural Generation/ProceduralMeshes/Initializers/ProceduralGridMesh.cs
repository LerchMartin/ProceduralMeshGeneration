using ProceduralMeshes.Jobs;
using ProceduralMeshes.Streams;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid = ProceduralMeshes.Generators.Grid;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGridMesh : MonoBehaviour
{
    Mesh mesh;

    private void Awake()
    {
        mesh = new Mesh() { name = "Procedural Grid Mesh " };

        GenerateMesh();

        GetComponent<MeshFilter>().mesh = mesh;

    }

    private void GenerateMesh()
    {
        Mesh.MeshDataArray meshDataArray = Mesh.AllocateWritableMeshData(1);
        Mesh.MeshData meshData = meshDataArray[0];

        MeshJob<Grid, SingleStream>.ScheduleParallel(mesh, meshData, default).Complete();

        Mesh.ApplyAndDisposeWritableMeshData(meshDataArray, mesh);
    }
}
