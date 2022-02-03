using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PlaneMeshGenerator : MonoBehaviour
{
    public int Width = 10;
    public int Height = 10;

    public float Granularity = 0.2f;

    Mesh mesh;

    List<Vector3> vertices;
    List<int> triangles;

    private void OnEnable()
    {
        mesh = new Mesh { name = "Procedurally created mesh" };

        ResetMeshAndValues();

        CreateShapeFromParameters();
        UpdateMesh();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
    }

    void CreateShapeFromParameters()
    {
        int currentCount = 0;
        int count = 0;

        for (float x = 0; x < Width; x += Granularity)
        {
            for (float z = 0; z < Height; z += Granularity)
            {
                CreateVertice(x, 0, z);
            }
        }

        int xSize = Convert.ToInt32(Width / Granularity);
        int vert = 0;

        for (int i = 0; i < xSize; i++)
        {
            CreateTriangleForSquare(vert,  xSize);
            vert++;
        }
    }

    private void CreateVertice(float x, float y, float z)
    {
        vertices.Add(new Vector3(x, y, z));
    }

    private void CreateTriangleForSquare(int vert, int xSize)
    {
        int[] quad = new int[] {
            vert + 0,
            vert + xSize + 1,
            vert + 1,
            vert + 1,
            vert + xSize + 1,
            vert + xSize + 2,
        };

        triangles.AddRange(quad);
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
        }.ToList();

        triangles = new int[]
        {
            0, 1, 2,
            1, 3, 2,
            3, 4, 2
        }.ToList();
    }

    private void OnDrawGizmos()
    {
        if (vertices is null)
            return;

        for (int i = 0; i < vertices.Count; i++)
        {
            Gizmos.DrawSphere(vertices[i], .05f);
        }
    }

    private void ResetMeshAndValues()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        mesh.Clear();
    }


}
