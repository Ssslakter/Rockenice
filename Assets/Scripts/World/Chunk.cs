using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMesh : MonoBehaviour
{


    [Range(8, 256)]
    public int resolution;
    [Range(1, 6)]
    public int simplificationFactor = 1;
    public float chunkLength;
    public bool autoUpdate;
    public float heightMultiplier = 5;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;
    public NoiseSettings settings;
    public Vector2 offset;
    [Header("checkpoint Settings")]
    public bool isCheckpoint;
    public float steep = -0.2f;
    public float localwidth = 4;
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
        float[,] heightMap = Noise.GenerateNoiseMap(resolution, resolution, settings, offset);
        float[,] snowMap = Noise.GenerateNoiseMap(resolution, resolution, settings, offset + new Vector2(100, 100));
        if (isCheckpoint)
        {
            heightMap = updateHeightMap(heightMap, steep, localwidth);
        }

        data = new MeshData(resolution, chunkLength, simplificationFactor);
        mesh = data.GenerateMesh(heightMap, heightMultiplier);
        if (isCheckpoint)
        {
            SpawnBigObjects();
        }
        SpawnSmallObjects();
        meshRenderer.material.mainTexture = GenerateColorMap(snowMap, mesh.normals);
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    float[,] updateHeightMap(float[,] map, float steepness, float _width)
    {
        float width = _width / heightMultiplier;
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                map[x, y] += Mathf.Clamp(steepness * (y - map.GetLength(1) / 2f) + width / 2, -width, 0);
            }
        }
        return map;
    }

    Texture2D GenerateColorMap(float[,] map, Vector3[] normalMap)
    {
        Texture2D texture = new Texture2D(resolution, resolution);
        Color[] colorMap = new Color[resolution * resolution];

        for (int j = 0; j < resolution; j++)
        {
            for (int i = 0; i < resolution; i++)
            {
                colorMap[resolution * j + i] = Color.Lerp(Color.black, Color.white, -map[i, j] + normalMap[resolution * j + i].y);
            }
        }
        texture.SetPixels(colorMap);
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }

    public void SpawnBigObjects()
    {
        foreach (GameObject item in Global.prefabs)
        {
            for (int i = 0; i < Random.Range(0, 5); i++)
            {
                GameObject obj = Instantiate(item);
                obj.transform.localScale = Vector3.one * 2;
                SpawnObjectRandomly(obj, 0.5f);
            }
        }
    }

    private void SpawnSmallObjects()
    {
        foreach (GameObject item in Global.prefabsInteractable)
        {
            for (int i = 0; i < Random.Range(0, 5); i++)
            {
                GameObject obj = Instantiate(item);
                Item itemScript = obj.AddComponent<Item>();
                itemScript.itemID = Global.nameToId[item.name];
                itemScript.itemSprite = Global.idToSprite[itemScript.itemID];
                if (Global.foodIdToNutritionalValue.ContainsKey(itemScript.itemID))
                {
                    Food foodData = obj.AddComponent<Food>();
                    itemScript.equipmentType = "Food";
                    itemScript.equipmentIndex = Global.foodIdToEquipmentIndex[itemScript.itemID];
                    foodData.health = Global.player.GetComponent<Inventory>().health;
                    foodData.nutritionalValue = Global.foodIdToNutritionalValue[itemScript.itemID];
                    foodData.keyForEat = KeyCode.Q;
                }
                Outline outline = obj.AddComponent<Outline>();
                outline.OutlineMode = Outline.Mode.OutlineVisible;
                SpawnObjectRandomly(obj, 0.05f);
            }
        }
    }

    private void SpawnObjectRandomly(GameObject gameObject, float flatnessCoef)
    {
        //flatnessCoef: 0 - где угодно 1 - на плоскости

        float dotProduct;
        int cnt = 0;
        flatnessCoef = flatnessCoef <= 1e-5f ? flatnessCoef + 1e-5f : flatnessCoef - 1e-5f;
        int randomIndex = Random.Range(0, mesh.vertices.Length), spawnPlace = randomIndex;
        do
        {
            randomIndex = Random.Range(0, mesh.vertices.Length);
            dotProduct = mesh.normals[randomIndex].normalized.y;
            spawnPlace = dotProduct > mesh.normals[spawnPlace].normalized.y ? randomIndex : spawnPlace;
            cnt++;
        }
        while (dotProduct < flatnessCoef || cnt >= 200);

        gameObject.transform.parent = transform;
        gameObject.transform.localPosition = mesh.vertices[spawnPlace];
        gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward * Random.Range(-1, 1) + Vector3.right * Random.Range(0.1f, 1), mesh.normals[spawnPlace].normalized);
    }

}