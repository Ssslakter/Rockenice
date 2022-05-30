using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMesh : MonoBehaviour
{
    //public enum DrawMode { NoiseMap, Mesh };
    //public DrawMode drawMode;

    [Range(8, 256)]
    public int resolution;
    [Range(1, 6)]
    public int simplificationFactor;
    public float chunkLength;
    [Header("Noise")]
    public int seed;
    public Vector2 offset;
    public bool autoUpdate;
    public float scale = 50;
    public AnimationCurve curve;


    //public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    List<GameObject> rocks;
    Mesh mesh;
    MeshData data;
    private void OnValidate()
    {
        if (resolution < 8)
        {
            resolution = 8;
        }
    }
    public void Generate()
    {
        NoiseSettings settings = new NoiseSettings();
        settings.seed = seed;
        settings.offset = offset;
        settings.scale = scale;
        float[,] heightMap = Noise.GenerateNoiseMap(resolution, resolution, settings, new Vector2(0, 0));
        meshRenderer.material.mainTexture = GenerateColorMap(heightMap);
        for (int i = 0; i < heightMap.GetLength(0); i++)
        {
            for (int j = 0; j < heightMap.GetLength(1); j++)
            {
                heightMap[i, j] += curve.Evaluate((float)i / heightMap.GetLength(0));
            }
        }
        data = new MeshData(resolution, chunkLength, simplificationFactor);
        mesh = data.GenerateMesh(heightMap);
        meshFilter.mesh = mesh;
    }

    Texture2D GenerateColorMap(float[,] map)
    {
        Texture2D texture = new Texture2D(resolution, resolution);
        Color[] colorMap = new Color[resolution * resolution];

        for (int j = 0; j < resolution; j++)
        {
            for (int i = 0; i < resolution; i++)
            {
                colorMap[resolution * j + i] = Color.Lerp(Color.black, Color.white, map[i, j]);
            }
        }
        texture.SetPixels(colorMap);
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }

    public void GenerateRocks()
    {
        rocks = new List<GameObject>(Resources.LoadAll<GameObject>("Rocks"));
        for (int i = 0; i < 100; i++)
        {
            SpawnRock(rocks[Random.Range(0, rocks.Count)]);
        }

    }

    private void SpawnRock(GameObject rock)
    {
        int randomIndex = Random.Range(0, mesh.vertices.Length), spawnPlace = randomIndex;
        for (int i = 0; i < 100; i++)
        {
            randomIndex = Random.Range(0, mesh.vertices.Length);
            spawnPlace = Vector3.Dot(mesh.normals[randomIndex], Vector3.right) > Vector3.Dot(mesh.normals[spawnPlace], Vector3.right) ? randomIndex : spawnPlace;
        }
        Instantiate(rock, mesh.vertices[spawnPlace], Quaternion.LookRotation(mesh.normals[spawnPlace], mesh.normals[spawnPlace]), transform);
    }

}

class MeshData
{
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;
    int triangleIndex = 0;
    int resolution;
    float chunkLength;
    int simplification;

    public MeshData(int _resolution, float _chunkLength, int sFactor)
    {
        simplification = ((_resolution - 1) % sFactor == 0) ? sFactor : 1;
        chunkLength = _chunkLength;
        resolution = _resolution / simplification;
        vertices = new Vector3[resolution * resolution];
        triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        uvs = new Vector2[resolution * resolution];
    }
    void Generate(float[,] heightMap)
    {
        int i = 0;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float xCoord = chunkLength * x / (resolution - 1) - chunkLength / 2;
                float yCoord = chunkLength * y / (resolution - 1) - chunkLength / 2;
                float zCoord = 5 * heightMap[x * simplification, y * simplification];
                vertices[i] = new Vector3(xCoord, zCoord, yCoord);
                uvs[i] = new Vector2(x * 1f / (resolution - 1), y * 1f / (resolution - 1));
                if (x != resolution - 1 && y != resolution - 1)
                {
                    AddTriangle(i, resolution + i, resolution + i + 1);
                    AddTriangle(i, resolution + i + 1, i + 1);
                }
                i++;
            }
        }
    }
    void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }
    public Mesh GenerateMesh(float[,] heightMap)
    {
        Generate(heightMap);
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }

}
