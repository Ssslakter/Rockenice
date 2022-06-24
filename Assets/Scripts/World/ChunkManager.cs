using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChunkManager : MonoBehaviour
{
    public Transform player;
    public const float viewRadius = 300;
    [Range(0, 90)]
    public float steepness;
    [Range(3, 20)]
    public int checkpointPeriod = 5;
    public ChunkParams chunkParams;
    int numberOfVisibleChunks;
    static Vector2 playerPosition;
    Dictionary<Vector2, Chunk> chunkDictionary = new Dictionary<Vector2, Chunk>();
    List<Chunk> visibleLastUpdate = new List<Chunk>();

    private void Start()
    {
        transform.Rotate(steepness, 0, 0);
        numberOfVisibleChunks = Mathf.RoundToInt(viewRadius / chunkParams.chunkLength);
        playerPosition = new Vector2(player.position.x, player.position.y);
        UpdateVisibleChunks();
    }
    private void Update()
    {
        playerPosition = new Vector2(player.position.x, player.position.y);
        UpdateVisibleChunks();
    }
    void UpdateVisibleChunks()
    {

        for (int i = 0; i < visibleLastUpdate.Count; i++)
        {
            visibleLastUpdate[i].ChangeVisibility(false);
        }
        visibleLastUpdate.Clear();

        int currX = Mathf.RoundToInt(player.position.x / chunkParams.chunkLength);
        int currY = Mathf.RoundToInt(player.position.y / chunkParams.chunkLength);

        for (int x = -numberOfVisibleChunks; x <= numberOfVisibleChunks; x++)
        {
            for (int y = -numberOfVisibleChunks; y <= numberOfVisibleChunks; y++)
            {
                Vector2Int worldPos = new Vector2Int(x + currX, y + currY);
                if (chunkDictionary.ContainsKey(worldPos))
                {
                    chunkDictionary[worldPos].UpdateChunk();
                    if (chunkDictionary[worldPos].isVisible())
                    {
                        visibleLastUpdate.Add(chunkDictionary[worldPos]);
                    }
                }
                else
                {
                    chunkDictionary.Add(worldPos, new Chunk(worldPos, transform, checkpointPeriod, chunkParams));
                }
            }
        }
    }

    [System.Serializable]
    public class ChunkParams
    {
        public float heightMultiplier;
        public float chunkLength;
        [Range(8, 256)]
        public int resolution;
        public Material terrainMaterial;
        public NoiseSettings noiseSettings;
        [Header("Single Checkpoint Settings")]
        public float steepOfCheckpoint = -0.2f;
        public float localCheckpointWidth = 4;
    }


    public class Chunk
    {
        Vector2 position;
        GameObject meshObject;
        Bounds bounds;
        ChunkMesh chunkMesh;

        public Chunk(Vector2Int worldPosition, Transform parent, int checkpointPeriod, ChunkParams parameters)
        {
            position = parameters.chunkLength * (Vector2)worldPosition;
            bounds = new Bounds(position, Vector2.one * parameters.chunkLength);
            meshObject = new GameObject("chunk");
            meshObject.transform.parent = parent;
            Vector3 shift;
            if (worldPosition.y > 0)
            {
                shift = parameters.localCheckpointWidth * new Vector3(0, 0, Mathf.FloorToInt((worldPosition.y + checkpointPeriod - 1) / checkpointPeriod));

            }
            else
            {
                shift = parameters.localCheckpointWidth * new Vector3(0, 0, Mathf.FloorToInt(worldPosition.y / checkpointPeriod));

            }
            meshObject.transform.localPosition = (Vector3)position + shift;
            meshObject.transform.localRotation = Quaternion.identity;

            chunkMesh = meshObject.AddComponent<ChunkMesh>();
            chunkMesh.meshFilter = meshObject.AddComponent<MeshFilter>();
            chunkMesh.meshRenderer = meshObject.AddComponent<MeshRenderer>();
            chunkMesh.meshCollider = meshObject.AddComponent<MeshCollider>();
            chunkMesh.isCheckpoint = worldPosition.y % checkpointPeriod == 0;
            chunkMesh.localwidth = parameters.localCheckpointWidth;
            chunkMesh.steep = parameters.steepOfCheckpoint;
            chunkMesh.heightMultiplier = parameters.heightMultiplier;
            chunkMesh.settings = parameters.noiseSettings;
            chunkMesh.offset = new Vector2((parameters.resolution - 1) * worldPosition.x, -(parameters.resolution - 1) * worldPosition.y);
            chunkMesh.meshRenderer.material = parameters.terrainMaterial;
            chunkMesh.resolution = parameters.resolution;
            chunkMesh.chunkLength = parameters.chunkLength;
            GenerateChunk();
            meshObject.SetActive(false);
        }

        void GenerateChunk()
        {
            chunkMesh.Generate();
        }

        public void UpdateChunk()
        {
            bool visible = Mathf.Sqrt(bounds.SqrDistance(playerPosition)) <= viewRadius;
            ChangeVisibility(visible);
        }
        public void ChangeVisibility(bool visibility)
        {
            meshObject.SetActive(visibility);
        }

        public bool isVisible()
        {
            return meshObject.activeSelf;
        }
    }

}
