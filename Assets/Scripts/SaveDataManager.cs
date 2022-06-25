using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

public static class SaveDataManager
{
    public static void SaveJsonData(SaveData sd)
    {

        if (WriteToFile("SaveData01.dat", sd.ToJson()))
        {
            Debug.Log("Save successful");
        }
    }

    public static SaveData LoadJsonData()
    {
        if (LoadFromFile("SaveData01.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            Debug.Log("Load complete");

            return sd;
        }
        return SaveData.Default;
    }

    private static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        try
        {
            File.WriteAllText(fullPath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to write to {fullPath} with exception {e}");
            return false;
        }
    }

    private static bool LoadFromFile(string a_FileName, out string result)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        try
        {
            result = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to read from {fullPath} with exception {e}");
            result = "";
            return false;
        }
    }
}
