using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Global
{
    static public GameObject[] prefabs;
    static public GameObject player;
    static public Dictionary<int, string> itemIds = new Dictionary<int, string> {
        { 0,"stone" },
        {1,"stick" },
        {3,"bow" },
        {10,"orange" }
    };


    static public void LoadPrefabs()
    {
        player = GameObject.Find("RigidBodyFPSController");
        prefabs = Resources.LoadAll<GameObject>("Polygon-Lite Survival Collection/NeedToSpawn");
    }
}
