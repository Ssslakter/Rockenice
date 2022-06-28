using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    public int id;
    public RequiredItem[] itemsNeeded;

    public Inventory inv;

    public GameObject craftedItem;

    public int craftedItemAmount;

    private void Start()
    {
        craftedItem = Global.idToCraftableItem[id];
    }
    public void CraftItem()
    {
        foreach(RequiredItem i in itemsNeeded)
        {
            if (inv.GetItemAmount(i.itemID) < i.amountNeeded)
                return;
        }
        foreach(RequiredItem i in itemsNeeded)
        {
            inv.RemoveItemAmount(i.itemID, i.amountNeeded);
        }

        foreach (Slot i in inv.slots)
        {
            i.CheckForItem();
        }        

        Item z = Instantiate(craftedItem, Vector3.zero, Quaternion.identity).GetComponent<Item>();
        z.amountInStack = craftedItemAmount;
        inv.AddItem(z);
        inv.craftPanel.GetComponent<CheckItemsForCreation>().UpdateItems();

    }
}

[System.Serializable]
public class RequiredItem
{
    public int itemID;
    public int amountNeeded;
}
