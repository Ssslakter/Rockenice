using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string equipmentType;
    public int equipmentIndex; 
    [Space]
    public Sprite itemSprite;
    [Space]
    public int amountInStack = 1;
    public int maxStackSize = 10;
    [Space]
    public int itemID;

    IEnumerator Example()
    {
        yield return new WaitForSecondsRealtime(1);
    }

    private void OnCollisionEnter()
    {
        Example();
        Destroy(gameObject.GetComponent<Rigidbody>());
    }
}
