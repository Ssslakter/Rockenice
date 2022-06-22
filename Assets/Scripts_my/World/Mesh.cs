using UnityEngine;

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
    void Generate(float[,] heightMap, float heightMultiplier)
    {
        int i = 0;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float xCoord = chunkLength * x / (resolution - 1) - chunkLength / 2;
                float yCoord = chunkLength * y / (resolution - 1) - chunkLength / 2;
                float zCoord = heightMultiplier * heightMap[x * simplification, y * simplification];
                vertices[i] = new Vector3(xCoord, yCoord, -zCoord);
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
    public Mesh GenerateMesh(float[,] heightMap, float heightMultiplier)
    {
        Generate(heightMap, heightMultiplier);
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }

}