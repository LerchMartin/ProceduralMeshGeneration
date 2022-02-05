using ProceduralMeshes.Jobs;
using ProceduralMeshes.Streams;
using UnityEngine;
using Grid = ProceduralMeshes.Generators.Grid;

public class ProceduralGridMesh : IProcedualMeshComponent
{
    [SerializeField, Range(1, 10)]
    int resolution = 1;

    private void Awake()
    {
        base.Awake();
    }

    protected override void GenerateMesh()
    {
        Mesh.MeshDataArray meshDataArray = Mesh.AllocateWritableMeshData(1);
        Mesh.MeshData meshData = meshDataArray[0];

        MeshJob<Grid, SingleStream>.ScheduleParallel(mesh, meshData, resolution, default).Complete();

        mesh.RecalculateBounds();

        Mesh.ApplyAndDisposeWritableMeshData(meshDataArray, mesh);
    }


}
