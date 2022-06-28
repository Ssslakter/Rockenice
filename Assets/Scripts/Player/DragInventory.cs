using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragInventory : MonoBehaviour
{
    public Inventory inv;

    GameObject curSlot;
    Item curSlotsItem;

    public Image followMouseImage;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject obj = GetObjectUnderMouse();
            if (obj)
            {
                obj.GetComponent<Slot>().slotsItem.amountInStack -= 1;
                if (obj.GetComponent<Slot>().slotsItem.amountInStack == 0)
                {
                    Destroy(obj.GetComponent<Slot>().slotsItem.gameObject);
                }

                obj.GetComponent<Slot>().CheckForItem();

                GameObject dropItem = Instantiate(obj, obj.transform);
                Outline outline = dropItem.GetComponent<Slot>().slotsItem.gameObject.GetComponent<Outline>();
                if (outline == null)
                {
                    outline = dropItem.GetComponent<Slot>().slotsItem.gameObject.AddComponent<Outline>();
                }
                outline.OutlineColor = Color.black;
                outline.OutlineWidth = 10;
                outline.enabled = true;

                dropItem.GetComponent<Slot>().slotsItem.gameObject.AddComponent<Rigidbody>();

                dropItem.GetComponent<Slot>().slotsItem.amountInStack = 1;
                dropItem.GetComponent<Slot>().DropItem();

                Destroy(dropItem);
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            curSlot = GetObjectUnderMouse();
        }
        else if (Input.GetMouseButton(0))
        {
            followMouseImage.transform.position = Input.mousePosition;
            if (curSlot && curSlot.GetComponent<Slot>().slotsItem)
            {
                followMouseImage.color = new Color(255, 255, 255, 255);
                followMouseImage.sprite = curSlot.transform.GetChild(1).GetComponent<Image>().sprite;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(curSlot && curSlot.GetComponent<Slot>().slotsItem)
            {
                curSlotsItem = curSlot.GetComponent<Slot>().slotsItem;
                GameObject newObj = GetObjectUnderMouse();
                if (newObj && newObj != curSlot)
                {

                    if (newObj.GetComponent<Slot>().slotsItem)
                    {
                        Item objectsItem = newObj.GetComponent<Slot>().slotsItem;
                        if(objectsItem.itemID == curSlotsItem.itemID && objectsItem.amountInStack != objectsItem.maxStackSize && !newObj.GetComponent<EquipmentSlot>())
                        {
                            curSlotsItem.transform.parent = null;
                            inv.AddItem(curSlotsItem, objectsItem);
                        }
                        else
                        {
                            objectsItem.transform.parent = curSlot.transform;
                            curSlotsItem.transform.parent = newObj.transform;
                        }
                    }
                    else
                    {
                        curSlotsItem.transform.parent = newObj.transform;
                    }
                }
            }
        }
        else
        {
            followMouseImage.sprite = null;
            followMouseImage.color = new Color(0, 0, 0, 0);
        }
    }

    GameObject GetObjectUnderMouse()
    {
        GraphicRaycaster rayCaster = GetComponent<GraphicRaycaster>();
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        rayCaster.Raycast(eventData, results);

        foreach (RaycastResult i in results)
        {
            if (i.gameObject.GetComponent<Slot>())
                return i.gameObject;
        }
        return null;
    }
}
