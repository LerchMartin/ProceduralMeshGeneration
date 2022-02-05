using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public abstract class IProcedualMeshComponent : MonoBehaviour
{
	protected Mesh mesh;

	protected abstract void GenerateMesh();

    protected virtual void Awake()
    {
        mesh = new Mesh() { name = "Procedural Grid Mesh " };

        GenerateMesh();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    void OnValidate() => enabled = true;

	void Update()
	{
		GenerateMesh();
		enabled = false;
	}
}
