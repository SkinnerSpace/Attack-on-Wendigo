using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    private const int QUAD_POINTS = 6;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    [SerializeField] private int xSize = 20;
    [SerializeField] private int zSize = 20;

    private int vert = 0;
    private int tris = 0;

    [SerializeField] private float scale = 16f;
    [SerializeField] private float mountainsSeed = 1f;
    [SerializeField] private float mountainsHeight = 16f;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void CreateShape()
    {
        CreateVertices();
        CreateQuads();
    }

    private void CreateVertices()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * mountainsSeed, z * mountainsSeed) * mountainsHeight;
                vertices[i] = new Vector3(x, y, z) * scale;
                i++;
            }
        }
    }

    private void CreateQuads()
    {
        AllocateSpaceForTriangles();

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
                CreateQuad();

            SwitchToNextLine();
        }
    }

    private void AllocateSpaceForTriangles()
    {
        triangles = new int[xSize * zSize * QUAD_POINTS];

        vert = 0;
        tris = 0;
    }

    private void CreateQuad()
    {
        CreateFirstTriangle();
        CreateSecondTriangle();

        SwitchToNextQuad();
    }

    private void CreateFirstTriangle()
    {
        triangles[tris + 0] = vert + 0;
        triangles[tris + 1] = vert + (xSize + 1);
        triangles[tris + 2] = vert + 1;
    }

    private void CreateSecondTriangle()
    {
        triangles[tris + 3] = vert + 1;
        triangles[tris + 4] = vert + (xSize + 1);
        triangles[tris + 5] = vert + (xSize + 2);
    }

    private void SwitchToNextQuad()
    {
        vert++;
        tris += QUAD_POINTS;
    }

    private void SwitchToNextLine()
    {
        vert++;
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
