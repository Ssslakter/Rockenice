using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image Healthbar;
    public Text text;
    public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController player;
    public float fallDamage;
    public GameObject deadText;

    private float fallDistance;
    private float lastPositionY;
    private void Start()
    {
        deadText.SetActive(false);
    }
    public void AddHealth(float value)
    {
        Healthbar.fillAmount = Mathf.Min(value / 100 + Healthbar.fillAmount, 1);
        text.text = Mathf.CeilToInt(Healthbar.fillAmount * 100).ToString();
    }

    public void RemoveHealth(float value)
    {
        Healthbar.fillAmount = Healthbar.fillAmount - value / 100;
        text.text = Mathf.CeilToInt(Healthbar.fillAmount * 100).ToString();

        if (Healthbar.fillAmount == 0)
        {
            text.gameObject.SetActive(false);
            deadText.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        checkOnFall();
    }

    void checkOnFall()
    {
        if (lastPositionY > player.transform.position.y)
        {
            fallDistance += lastPositionY - player.transform.position.y;
        }

        lastPositionY = player.transform.position.y;

        if (player.Grounded)
        {
            if (fallDistance >= 7)
            {
                RemoveHealth(fallDistance * fallDamage);
            }
            fallDistance = 0;
        }
    }
}
