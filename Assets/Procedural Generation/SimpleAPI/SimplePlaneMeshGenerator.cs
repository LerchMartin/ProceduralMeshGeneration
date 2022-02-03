using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SimplePlaneMeshGenerator : MonoBehaviour
{
    public int XSize = 10;
    public int ZSize = 10;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    private void Start()
    {
        mesh = new Mesh();

        CreateMesh();
        UpdateMesh();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    void CreateMesh()
    {
        vertices = new Vector3[(XSize + 1) * (ZSize + 1)];

        for (int i = 0, z = 0; z < ZSize; z++)
        {
            for (int x = 0; x < XSize; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        int vert = 0;
        int tris = 0;

        triangles = new int[XSize * ZSize * 6];

        for (int i = 0; i < XSize; i++)
        {
            triangles[tris + 0] = vert + 0;
            triangles[tris + 1] = vert + XSize + 1;
            triangles[tris + 2] = vert + 1;
            triangles[tris + 3] = vert + 1;
            triangles[tris + 4] = vert + XSize + 1;
            triangles[tris + 5] = vert + XSize + 2;

            vert++;
            tris += 6;
        }

    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    void OnDrawGizmos()
    {
        if(mesh is null || mesh.vertices is null)
            return;

        foreach(var vertice in mesh.vertices)
        {
            Gizmos.DrawWireSphere(vertice, .1f);
        }
    }

}
