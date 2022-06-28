using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public HealthBar health;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health.Healthbar.fillAmount = 0;
            health.text.gameObject.SetActive(false);
            health.deadText.SetActive(true);
            health.OpenDeathScreen();
        }
    }
}
