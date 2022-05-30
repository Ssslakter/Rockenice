using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public Transform player;
    public int radius;
    [Range(8, 256)]
    public int resolution;
    [Range(0, 90)]
    public float steepness;
    public float chunkLength;
    public AnimationCurve curve;
    public Material terrainMaterial;
    List<ChunkMesh> chunks;


    private void Start()
    {
        transform.Rotate(0, 0, steepness);
        chunks = new List<ChunkMesh>();
        CreateMap();
    }
    private void Update()
    {
        //CreateMap();
    }
    void CreateMap()
    {
        Vector3Int playerPosition = Vector3Int.FloorToInt(new Vector3(player.position.x / chunkLength, 0, player.position.z / chunkLength));
        for (int x = -4; x <= 4; x++)
        {
            for (int y = -4; y <= 4; y++)
            {
                Vector3 pos = chunkLength * (playerPosition + new Vector3(x, 0, y));
                int simplificationFactor = Simplification((pos - player.position).magnitude);
                UpdateMap(pos, simplificationFactor);
            }
        }
    }

    void UpdateMap(Vector3 position, int simplificationFactor)
    {
        bool isChunkExists = false;
        for (int i = 0; i < chunks.Count; i++)
        {
            if (chunks[i].transform.localPosition.x == position.x && chunks[i].transform.localPosition.z == position.z)
            {
                isChunkExists = true;
                if (chunks[i].simplificationFactor != simplificationFactor)
                {
                    UpdateChunk(chunks[i], simplificationFactor);
                }
            }
        }
        if (!isChunkExists && simplificationFactor != 0)
        {
            GenerateChunk(position, simplificationFactor);
        }
    }

    void GenerateChunk(Vector3 position, int simplificationFactor)
    {
        GameObject currChunk = new GameObject("chunk");
        ChunkMesh chunkMesh = currChunk.AddComponent<ChunkMesh>();
        chunkMesh.meshFilter = currChunk.AddComponent<MeshFilter>();
        chunkMesh.meshRenderer = currChunk.AddComponent<MeshRenderer>();
        chunkMesh.meshCollider = currChunk.AddComponent<MeshCollider>();

        int worldPositionX = (int)(position.x / chunkLength);
        int worldPositionZ = (int)(position.z / chunkLength);
        currChunk.transform.parent = transform;
        currChunk.transform.localPosition = position - worldPositionX * 5 * curve.Evaluate(0) * Vector3.up;
        currChunk.transform.localRotation = Quaternion.identity;
        chunkMesh.offset = new Vector2((resolution - 1) * worldPositionX, -(resolution - 1) * worldPositionZ);
        chunkMesh.meshRenderer.material = terrainMaterial;
        chunkMesh.resolution = resolution;
        chunkMesh.simplificationFactor = simplificationFactor;
        chunkMesh.chunkLength = chunkLength;
        chunkMesh.curve = curve;
        chunkMesh.Generate();
        chunks.Add(chunkMesh);
    }

    void UpdateChunk(ChunkMesh chunk, int simplificationFactor)
    {
        chunk.simplificationFactor = simplificationFactor;
        if (simplificationFactor == 0)
        {
            chunk.gameObject.SetActive(false);
        }
        else
        {
            chunk.gameObject.SetActive(true);
            chunk.Generate();
        }

    }



    int Simplification(float distance)
    {
        if (distance < radius / 6)
        {
            return 1;
        }
        if (distance < radius / 5)
        {
            return 2;
        }
        if (distance < radius / 4)
        {
            return 3;
        }
        if (distance < radius / 3)
        {
            return 4;
        }
        if (distance < radius / 2)
        {
            return 5;
        }
        if (distance < radius)
        {
            return 6;
        }
        return 0;
    }
}
