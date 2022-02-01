using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PlaneMeshGenerator : MonoBehaviour
{
    public int Width = 10;
    public int Height = 10;

    public float Granularity = 0.3f;

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    private void OnEnable()
    {
        mesh = new Mesh { name = "Procedurally created mesh" };

        CreateShape();
        UpdateMesh();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    void CreateShapeFromParameters()
    {

    }

    void CreateShape()
    {
        vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 1),
            new Vector3(2, 0, 1),
        };

        triangles = new int[]
        {
            0, 1, 2,
            1, 3, 2,
            3, 4, 2
        };
    }

    private void OnDrawGizmos()
    {
        foreach(var vertice in vertices)
        {
            Gizmos.DrawSphere(vertice, 0.05f);
        }
    }
}
