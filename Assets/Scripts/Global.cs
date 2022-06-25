using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Localization;

public class Global : MonoBehaviour
{
    static public GameObject[] prefabs;
    static public GameObject[] prefabsInteractable;
    static public GameObject player;
    static public GameObject signForItems;
    static public LocalizedString str;
    public Transform signParent;

    static public Dictionary<string, int> nameToId = new Dictionary<string, int> {
        {"Rock_01" , 0},
        {"Rock_02" , 0},
        {"Rock_03" , 0},
        {"Rock_04" , 0},
        {"Rock_05" , 0},
        {"Branch_01" , 1},
        {"Mushroom_01" , 2},
        {"Mushroom_02" , 2},
        {"Bonfire_01" , 4},
        {"SM_Scrap_Metal_02" , 5},
        {"SM_Scrap_Metal_03" , 5},
        {"SM_Woods_02" , 6}
    };


    static public Dictionary<int, LocalizedString> itemIds = new Dictionary<int, LocalizedString> {
        { 0, new LocalizedString("UI text","Stone") },
        { 1,new LocalizedString("UI text","Stick") },
        { 2,new LocalizedString("UI text","Mushroom") },
        { 3,new LocalizedString("UI text","Bow") },
        { 4,new LocalizedString("UI text","Campfire") },
        { 5,new LocalizedString("UI text","Scrap") },
        { 6,new LocalizedString("UI text","Planks") },
        { 10,new LocalizedString("UI text","Orange") }
    };
    static public Dictionary<int, FullScreenMode> screenModes = new Dictionary<int, FullScreenMode>
    {

        {0,FullScreenMode.Windowed },
         {1,FullScreenMode.FullScreenWindow },
        {2,FullScreenMode.ExclusiveFullScreen }
    };

    public void Awake()
    {
        player = GameObject.Find("RigidBodyFPSController");
        signForItems = signParent.Find("SignForItems").gameObject;
        prefabs = Resources.LoadAll<GameObject>("NeedToSpawn/Default");
        prefabsInteractable = Resources.LoadAll<GameObject>("NeedToSpawn/WithOutline");
    }
}
