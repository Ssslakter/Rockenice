using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    public RequiredItem[] itemsNeeded;

    public Inventory inv;

    public GameObject craftedItem;
    public int craftedItemAmount;

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

        Item z = Instantiate(craftedItem, Vector3.zero, Quaternion.identity).GetComponent<Item>();
        z.amountInStack = craftedItemAmount;

        inv.AddItem(z);
    }
}

[System.Serializable]
public class RequiredItem
{
    public int itemID;
    public int amountNeeded;
}
