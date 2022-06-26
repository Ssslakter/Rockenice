using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float volume;
    public int fullScreenMode;
    public int currentLanguage;
    public Vector3 playerPosition;
    public string maxScore;
    public float hp;
    public bool isFiniteWorld;
    public int resolution;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
    public static SaveData Default = DefaultData();
    static SaveData DefaultData()
    {
        SaveData instance = new SaveData();
        instance.playerPosition = new Vector3(-13.4f, 11.6f, 12.3f);
        instance.maxScore = "0";
        instance.volume = 0.5f;
        instance.fullScreenMode = 0;
        instance.currentLanguage = 0;
        instance.hp = 1;
        instance.isFiniteWorld = true;
        instance.resolution = 0;
        return instance;
    }
}
