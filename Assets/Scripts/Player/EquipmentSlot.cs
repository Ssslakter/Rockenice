using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    public string equipmentType;

    public GameObject[] items;

    public void Equip()
    {
        if(transform.childCount <= 1)
        {
            foreach (GameObject i in items)
                i.SetActive(false);
            return;
        }

        int num = transform.GetChild(1).GetComponent<Item>().equipmentIndex;
        for(int i = 0; i < items.Length; i++)
        {
            if (i == num)
                items[i].SetActive(true);
            else
                items[i].SetActive(false);
        }
    }
}
