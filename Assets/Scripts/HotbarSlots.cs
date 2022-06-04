using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarSlots : MonoBehaviour
{
    public GameObject[] slot;
    public GameObject[] unstaticItems;
    public int number;
    public GameObject panel;

    public static GameObject[] items;

    public KeyCode key;
    public KeyCode key2;
    public KeyCode key3;
    public KeyCode key4;
    public KeyCode key5;
    public KeyCode key6;
    public KeyCode key7;
    public KeyCode key8;

    // Start is called before the first frame update
    void Start()
    {
        if (unstaticItems.Length > 0)
            items = unstaticItems;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            number = 0;
            Equip();
        }
        if (Input.GetKeyDown(key2))
        {
            number = 1;
            Equip();
        }
        if (Input.GetKeyDown(key3))
        {
            number = 2;
            Equip();
        }
        if (Input.GetKeyDown(key4))
        {
            number = 3;
            Equip();
        }
        if (Input.GetKeyDown(key5))
        {
            number = 4;
            Equip();
        }
        if (Input.GetKeyDown(key6))
        {
            number = 5;
            Equip();
        }
        if (Input.GetKeyDown(key7))
        {
            number = 6;
            Equip();
        }
        if (Input.GetKeyDown(key8))
        {
            number = 7;
            Equip();
        }       
    }

    void Equip()
    {
        for (int y = 0; y < slot.Length; y++)
        {
            if (y == number)
            {
                panel.transform.position = slot[y].transform.position;
                if (slot[y].transform.childCount > 1)
                {
                    Item item = slot[y].transform.GetChild(1).GetComponent<Item>();

                    if (item.equipmentType == "Bow")
                    {
                        for (int i = 0; i < items.Length; i++)
                        {
                            if (i == item.equipmentIndex)
                            {
                                items[i].SetActive(!items[i].activeInHierarchy);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        {
                            items[i].SetActive(false);
                        }
                    }
                }
                break;
            }
        }
    }
}
