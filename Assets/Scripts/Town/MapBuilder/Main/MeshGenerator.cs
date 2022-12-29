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
    GeoMap geoMap;

    private int width;
    private int height;

    private int vert = 0;
    private int tris = 0;

    private float scale = 100f;
    [SerializeField] private float mountainsSeed = 1f;
    [SerializeField] private float mountainsHeight = 16f;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void GenerateTerrain(GeoMap geoMap)
    {
        this.geoMap = geoMap;

        width = geoMap.width;
        height = geoMap.height;
        //scale = geoMap.scale;

        CreateShape();
        UpdateMesh();
        Debug.Log("Generate");
    }

    private void CreateShape()
    {
        CreateVertices(geoMap);
        CreateQuads();
    }

    private void CreateVertices(GeoMap geoMap)
    {
        vertices = new Vector3[(width + 1) * (height + 1)];

        for (int i = 0, z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                float y = geoMap.GetAltitude(x, z) * 10f;
                vertices[i] = new Vector3(x, y, z) * scale;
                i++;
            }
        }
    }

    private void CreateQuads()
    {
        AllocateSpaceForTriangles();

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
                CreateQuad();

            SwitchToNextLine();
        }
    }

    private void AllocateSpaceForTriangles()
    {
        triangles = new int[width * height * QUAD_POINTS];

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
        triangles[tris + 1] = vert + (width + 1);
        triangles[tris + 2] = vert + 1;
    }

    private void CreateSecondTriangle()
    {
        triangles[tris + 3] = vert + 1;
        triangles[tris + 4] = vert + (width + 1);
        triangles[tris + 5] = vert + (width + 2);
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
