using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckItemsForCreation : MonoBehaviour
{
    // Start is called before the first frame update
    public Inventory inventory;
    public GameObject[] items;
    public GameObject craftPanel;

    private List<GameObject> openedItems = new List<GameObject>();
    private List<GameObject> children = new List<GameObject>();

    public void UpdateItems()
    {
        foreach (GameObject craftItem in items)
        {
            bool canCraft = true;
            foreach (RequiredItem i in craftItem.transform.GetChild(0).GetComponent<CraftingSlot>().itemsNeeded)// getting list of needed items
            {
                if (inventory.GetItemAmount(i.itemID) == 0)
                {
                    canCraft = false;
                    break;
                }
            }

            if (canCraft)
            {
                if (!openedItems.Contains(craftItem))
                {
                    var item = Instantiate(craftItem);
                    item.transform.SetParent(craftPanel.transform);
                    item.GetComponentInChildren<CraftingSlot>().inv = inventory;
                    openedItems.Add(craftItem);
                }
            }

        }
    }
}
