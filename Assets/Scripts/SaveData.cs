using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float volume;
    public int fullScreenMode;
    public int currentLanguage;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }

    public static SaveData Default()
    {
        SaveData instance = new SaveData();
        instance.volume = 0.5f;
        instance.fullScreenMode = 0;
        instance.currentLanguage = 0;
        return instance;
    }
}
