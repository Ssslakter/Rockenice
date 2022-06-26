using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    float speed;
    public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController rb;
    public AudioClip running;
    public AudioClip walking;
    private AudioSource audioS;
    void Start()
    {
        audioS = rb.gameObject.GetComponent<AudioSource>();
    }


    void Update()
    {
        speed = rb.Velocity.magnitude;
        if (rb.Grounded && speed > 1 && audioS.isPlaying == false)
        {
            audioS.clip = walking;
            audioS.volume = Random.Range(0.8f, 1);
            audioS.pitch = Random.Range(0.8f, 1.1f);
            audioS.Play();
        }
        else if (rb.Grounded && speed > 10 && audioS.isPlaying == false)
        {
            audioS.clip = running;
            audioS.volume = Random.Range(0.8f, 1);
            audioS.pitch = Random.Range(0.8f, 1.1f);
            audioS.Play();
        }
    }
}
