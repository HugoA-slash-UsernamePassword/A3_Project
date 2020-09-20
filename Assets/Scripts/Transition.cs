﻿//Todo: 
//Change trigger from anim to code
//Move animator to Sprite gameobject and fix animations accordingly
//1 bone


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Transition : MonoBehaviour
{
    public bool trigger;
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles = new ParticleSystem.Particle[100];
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger) { ps.Play(); trigger = false; }
        if(ps.isPlaying)
        {
            ps.GetParticles(particles);
            for (int i = 0; i < particles.Length; i++)
            {
                //particles[i].position = Vector3.Lerp(particles[i].position, transform.position, particles[i].velocity.magnitude);
                if(Vector3.Distance(particles[i].position, transform.position) > 0.1f)
                particles[i].position = Vector2.Lerp(particles[i].position, transform.position, (1 + (particles[i].remainingLifetime/particles[i].startLifetime * 2)) * Time.deltaTime);                
                //particles[i].position = Vector3.Lerp(particles[i].position, transform.position, 0.1f * Time.deltaTime);
            }
            ps.SetParticles(particles, particles.Length);
        }
    }
}
