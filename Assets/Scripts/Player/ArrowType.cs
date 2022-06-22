using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    void ChangeArrow()
    {
        panel.transform.position = slots[type].transform.position;
        bow.arrow = arrows[type];
    }
}
