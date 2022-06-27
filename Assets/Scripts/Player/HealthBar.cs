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
    public AudioClip hurtSound;
    public static float hp;

    private bool firstFrame = true;
    private float fallDistance;
    private float lastPositionY;
    private void Start()
    {
        lastPositionY = player.transform.position.y;
        deadText.SetActive(false);
    }

    private void Update()
    {
        if (firstFrame)
        {
            Healthbar.fillAmount = hp;
            text.text = Mathf.CeilToInt(Healthbar.fillAmount * 100).ToString();
            firstFrame = false;
        }
    }
    public void AddHealth(float value)
    {
        Healthbar.fillAmount = Mathf.Min(value / 100 + Healthbar.fillAmount, 1);
        text.text = Mathf.CeilToInt(Healthbar.fillAmount * 100).ToString();
        hp = Healthbar.fillAmount;
    }

    private void PlaySound()
    {
        AudioSource audioSource = player.gameObject.GetComponent<AudioSource>();
        audioSource.clip = hurtSound;
        audioSource.volume = Random.Range(0.8f, 1);
        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.Play();
    }

    public void RemoveHealth(float value)
    {
        Healthbar.fillAmount = Healthbar.fillAmount - value / 100;
        hp = Healthbar.fillAmount;
        text.text = Mathf.CeilToInt(Healthbar.fillAmount * 100).ToString();
        PlaySound();
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
