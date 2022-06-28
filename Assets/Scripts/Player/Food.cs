using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float nutritionalValue;
    private int idFood;
    public KeyCode keyForEat;
    //public Inventory inventory;
    public HealthBar health;
    //public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController player;

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
            Global.player.GetComponent<Inventory>().RemoveItemAmount(idFood, 1);

            if (nutritionalValue > 0)
            {
                health.AddHealth(nutritionalValue);
            }
            else
            {
                health.RemoveHealth(nutritionalValue);
            }
            Global.player.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().movementSettings.AddStamina(nutritionalValue);

            if (Global.player.GetComponent<Inventory>().GetItemAmount(idFood) == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
