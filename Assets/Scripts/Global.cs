using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Global
{
    static public GameObject[] prefabs;
    static public GameObject player;
    static public GameObject signForItems;

    static public Dictionary<int, string> itemIds = new Dictionary<int, string> {
        { 0, "stone" },
        { 1, "stick" },
        { 3, "bow"   },
        { 10, "orange" }
    };
    static public Dictionary<int, FullScreenMode> screenModes = new Dictionary<int, FullScreenMode>
    {
        {0,FullScreenMode.Windowed },
         {1,FullScreenMode.FullScreenWindow },
        {2,FullScreenMode.ExclusiveFullScreen }

    };


    static public void LoadPrefabs()
    {
        player = GameObject.Find("RigidBodyFPSController");
        signForItems = GameObject.Find("SignForItems");
        prefabs = Resources.LoadAll<GameObject>("Polygon-Lite Survival Collection/NeedToSpawn");
    }
}
