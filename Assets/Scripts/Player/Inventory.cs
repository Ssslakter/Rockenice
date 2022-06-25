using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryObject;
    public GameObject aimPoint;
    public GameObject craftPanel;
    public float distance = 2f;

    public Slot[] slots;

    public Slot[] equipSlots;

    void Start()
    {
        inventoryObject.SetActive(false);

        foreach(Slot i in slots)
        {
            i.CustomStart();
        }
        foreach (Slot i in equipSlots)
        {
            i.CustomStart();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryObject.activeSelf == false)
            {
                inventoryObject.SetActive(true);
                aimPoint.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = false;
                craftPanel.GetComponent<CheckItemsForCreation>().UpdateItems();
            }
            else
            {
                inventoryObject.SetActive(false);
                aimPoint.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance))
            {
                if (hit.collider.gameObject.GetComponent<Item>())
                {
                    AddItem(hit.collider.gameObject.GetComponent<Item>());
                    hit.collider.gameObject.GetComponent<Outline>().OnMouseExit();
            }
        }
        }

        foreach (Slot i in slots)
        {
            i.CheckForItem();
        }
    }

    public int GetItemAmount(int id)
    {
        int num = 0;
        foreach(Slot i in slots)
        {
            if (i.slotsItem)
            {
                Item z = i.slotsItem;
                if (z.itemID == id)
                    num += z.amountInStack;
            }
        }
        return num;
    }

    public void RemoveItemAmount(int id, int amountToRemove)
    {
        List<Slot> list = new List<Slot> ();

        foreach(Slot i in slots)
        {
            if (amountToRemove <= 0)
                return;

            if (i.slotsItem)
            {
                if (i.slotsItem.itemID == id)
                {
                    list.Add(i);
                }
            }
        }

        foreach (Slot i in list)
        {
            Item z = i.slotsItem;

            int amountThatCanRemoved = z.amountInStack;
            if (amountThatCanRemoved <= amountToRemove)
            {
                Destroy(z.gameObject);
                amountToRemove -= amountThatCanRemoved;
                z.amountInStack -= amountThatCanRemoved;
            }
            else
            {
                z.amountInStack -= amountToRemove;
                amountToRemove = 0;
                
                break;
            }
        }
        foreach (Slot i in slots)
        {
            i.CheckForItem();
        }
    }

    public void AddItem(Item itemToBeAdded, Item startingItem = null)
    {
        int amountInStack = itemToBeAdded.amountInStack;
        List<Item> stackableItems = new List<Item>();
        List<Slot> emptySlots = new List<Slot>();

        if (startingItem && startingItem.itemID == itemToBeAdded.itemID && startingItem.amountInStack < startingItem.maxStackSize)
            stackableItems.Add(startingItem);

        foreach (Slot i in slots)
        {
            if (i.slotsItem != null)
            {
                Item z = i.slotsItem;
                if (z.itemID == itemToBeAdded.itemID && z.amountInStack < z.maxStackSize && z != startingItem)
                    stackableItems.Add(z);
            }
            else
            {
                emptySlots.Add(i);
            }
        }

        foreach (Item i in stackableItems)
        {
            int amountThatCanbeAdded = i.maxStackSize - i.amountInStack;
            if(amountInStack <= amountThatCanbeAdded)
            {
                i.amountInStack += amountInStack;
                Destroy(itemToBeAdded.gameObject);
                return;
            }
            else
            {
                i.amountInStack = i.maxStackSize;
                amountInStack -= amountThatCanbeAdded;
            }
        }

        itemToBeAdded.amountInStack = amountInStack;
        if(emptySlots.Count > 0)
        {
            itemToBeAdded.transform.parent = emptySlots[0].transform;
            itemToBeAdded.gameObject.SetActive(false);
        }
    }
}
