using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform arm;
    public Bow bow;
    Bow equipedBow = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (equipedBow == null)
            {
                EquipBow(bow);
            }
            else
            {
                Destroy(equipedBow.gameObject);
            }
        }    
    }

    void EquipBow(Bow bowToEquip)
    {
        equipedBow = Instantiate(bowToEquip, arm.position, arm.rotation);
        equipedBow.transform.parent = arm;
    }
}
