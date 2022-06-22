using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSystem : MonoBehaviour
{

    public Transform player;
    private ParticleSystem system;
    void Start()
    {
        system = transform.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + Vector3.up * 20;
        if (Input.GetKeyDown(KeyCode.K))
        {
            MakeBlizzard();
        }
    }

    void MakeBlizzard()
    {
        var force = system.forceOverLifetime;
        force.x = 4;
        force.y = 4;
        var module = system.main;
        module.simulationSpeed = 3.5f;
        module.maxParticles = 2000;
        var noise = system.noise;
        noise.enabled = true;
        var emission = system.emission;
        emission.rateOverTime = 700;
    }
}
