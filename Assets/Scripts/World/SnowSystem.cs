using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSystem : MonoBehaviour
{

    public Transform player;
    public Fog fog;
    public AudioSource source;
    private ParticleSystem system;
    private bool blizzardStarted = false;
    private bool peakVal = false;
    void Start()
    {
        system = transform.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + Vector3.up * 20;
        if (Random.Range(0, 800) == 30 && blizzardStarted && peakVal)
        {
            blizzardStarted = false;
        }
        if (Random.Range(0, 800) == 44 && !blizzardStarted && peakVal)
        {
            blizzardStarted = true;
        }
        if (blizzardStarted)
        {
            source.volume = Mathf.Min(0.4f, source.volume + Time.deltaTime / 40);
            fog.fogDensity = Mathf.Min(0.08f, fog.fogDensity + Time.deltaTime / 500);
            if (fog.fogDensity > 0.01f)
            {
                MakeBlizzard();
            }
            if (fog.fogDensity >= 0.1f)
            {
                peakVal = true;
            }
        }
        else
        {
            StopBlizzard();
            source.volume = Mathf.Max(0, source.volume - Time.deltaTime / 40);
            fog.fogDensity = Mathf.Max(0, fog.fogDensity - Time.deltaTime / 500);
            if (fog.fogDensity <= 0)
            {
                peakVal = true;
            }
        }
    }

    void StopBlizzard()
    {
        var force = system.forceOverLifetime;
        force.enabled = false;
        var module = system.main;
        module.simulationSpeed = 1;
        module.maxParticles = 1000;
        var noise = system.noise;
        noise.enabled = false;
        var emission = system.emission;
        emission.rateOverTime = 300;
    }

    void MakeBlizzard()
    {
        var force = system.forceOverLifetime;
        force.x = 5;
        force.y = 5;
        var module = system.main;
        module.simulationSpeed = 3.6f;
        module.maxParticles = 2200;
        var noise = system.noise;
        noise.enabled = true;
        var emission = system.emission;
        emission.rateOverTime = 800;
    }
}
