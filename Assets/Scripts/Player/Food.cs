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
            inventory.RemoveItemAmount(idFood, 1);

            Debug.Log("Скушано");

            if (inventory.GetItemAmount(idFood) == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
