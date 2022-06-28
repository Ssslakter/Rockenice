using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarSlots : MonoBehaviour
{
    public GameObject[] slot;
    public GameObject[] unstaticItems;
    public int number;
    public GameObject panel;
    public GameObject arrowType;

    public static GameObject[] items;

    public KeyCode key;
    public KeyCode key2;
    public KeyCode key3;
    public KeyCode key4;
    public KeyCode key5;

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
    }

    void UnEquipAll()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(false);
        }
    }

    void Equip()
    {
        UnEquipAll();
        panel.transform.position = slot[number].transform.position;
        if (slot[number].transform.childCount > 2)
        {
            Item item = slot[number].transform.GetChild(2).GetComponent<Item>();

            if (item.equipmentType == "Bow")
            {
                items[item.equipmentIndex].SetActive(!items[item.equipmentIndex].activeInHierarchy);
                arrowType.SetActive(true);
            }
            if (item.equipmentType == "Food")
            {
                items[item.equipmentIndex].SetActive(!items[item.equipmentIndex].activeInHierarchy);
            }

        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].SetActive(false);
            }
        }
    }
}
