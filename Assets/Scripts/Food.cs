using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float nutritionalValue;
    private int idFood;
    public KeyCode keyForEat;
    public Inventory inventory;

    private void Start()
    {
        var stats = gameObject.GetComponent<Item>();
        idFood = stats.itemID;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyForEat))
        {
            Debug.Log(idFood);
            Debug.Log(inventory.GetItemAmount(idFood));
            inventory.RemoveItemAmount(idFood, 1);
            new WaitForSeconds(5);
            Debug.Log("Скушано");
            Debug.Log(inventory.GetItemAmount(idFood));
            if (inventory.GetItemAmount(idFood) == 0)
            {
                gameObject.SetActive(false);
            }
            foreach (Slot i in inventory.slots)
            {
                i.CheckForItem();
            }

        }
    }
}
