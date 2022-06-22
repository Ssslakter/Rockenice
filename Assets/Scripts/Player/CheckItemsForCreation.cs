using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItemsForCreation : MonoBehaviour
{
    // Start is called before the first frame update
    public Inventory inventory;
    public GameObject[] items;
    public GameObject craftPanel;

    public void UpdateItems()
    {

        List<GameObject> allChildren = new List<GameObject>();

        foreach (Transform child in craftPanel.transform)
        {
            allChildren.Add(child.gameObject);
        }

        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject, true);
        }

        foreach(GameObject craftItem in items)
        {
            bool canCraft = true;
            foreach (RequiredItem i in craftItem.transform.GetChild(0).GetComponent<CraftingSlot>().itemsNeeded)// getting list of needed items
            {
                if (inventory.GetItemAmount(i.itemID) == 0)
                {
                    canCraft = false;
                    return;
                }
            }

            if (canCraft)
            {
                var item = Instantiate(craftItem);
                item.transform.parent = craftPanel.transform;
                if (inventory == null)
                {
                    Debug.Log("Капец...");
                }
                else
                {
                    Debug.Log("Как бы не конец, но капец...");
                    item.GetComponentInChildren<CraftingSlot>().inv = inventory;
                }
            }
        }
    }
}
