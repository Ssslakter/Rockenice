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

    public Texture2D[] textures;
    public GameObject[] craftableItems;

    private static bool flagForSprites = true;

    static public Dictionary<int, Sprite> idToSprite = new Dictionary<int, Sprite>();

    static public Dictionary<int, GameObject> idToCraftableItem = new Dictionary<int, GameObject>();

    static public Dictionary<string, int> nameToId = new Dictionary<string, int> {
        {"Rock_01" , 0},
        {"Rock_02" , 0},
        {"Rock_03" , 0},
        {"Rock_04" , 0},
        {"Rock_05" , 0},
        {"Branch_01" , 1},
        {"Mushroom_02" , 2},
        {"Mushroom_01" , 11},
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
        { 10,new LocalizedString("UI text","Orange") },
        { 11, new LocalizedString("UI text", "Red mushroom")},

    };

    static public Dictionary<int, int> foodIdToNutritionalValue = new Dictionary<int, int> {
        { 2, 10 },
        { 11, -10 },
    };

    static public Dictionary<int, int> foodIdToEquipmentIndex = new Dictionary<int, int> {
        { 2, 2 },
        { 11, 3 },
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
        textures = Resources.LoadAll<Texture2D>("Sprites");
        craftableItems = Resources.LoadAll<GameObject>("Prefabs/CraftableItems");
        


        if (flagForSprites)
        {
            foreach(Texture2D texture in textures)
            {
                Sprite mySprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                idToSprite.Add(int.Parse(texture.name), mySprite);
            }

            foreach(GameObject item in craftableItems)
            {
                idToCraftableItem.Add(int.Parse(item.name), item);
            }
            flagForSprites = false;
        }
    }
}
