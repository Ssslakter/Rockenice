using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowType : MonoBehaviour
{
    // Start is called before the first frame update

    public KeyCode key;
    public KeyCode key2;
    public Bow bow;
    public GameObject changeArrowTypeObj;
    public GameObject panel;
    public GameObject[] slots;
    public GameObject[] arrows;

    public TMP_Text first;
    public TMP_Text second;

    private int type;
    void Start()
    {
        changeArrowTypeObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (bow.gameObject.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(key))
            {
                type = 0;
                ChangeArrow();
            }
            if (Input.GetKeyDown(key2))
            {
                type = 1;
                ChangeArrow();
            }
        }
        else
        {
            if (changeArrowTypeObj.activeSelf == true)
            {
                changeArrowTypeObj.SetActive(false);
            }
        }
        UpdateText();
    }

    void ChangeArrow()
    {
        panel.transform.position = slots[type].transform.position;
        bow.arrow = arrows[type];
    }

    void UpdateText()
    {
        first.text = Global.player.GetComponent<Inventory>().GetItemAmount(arrows[0].GetComponent<Item>().itemID).ToString();
        second.text = Global.player.GetComponent<Inventory>().GetItemAmount(arrows[1].GetComponent<Item>().itemID).ToString();
    }
}
