//Todo: 
//Move animator to Sprite gameobject and fix animations accordingly


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Transition : MonoBehaviour
{
    //Animator
    private int xDir, yDir;
    public Animator anim;
    [SerializeField]
    private testCases testCase;
    //Particles
    public bool trigger;
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles = new ParticleSystem.Particle[100];
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        InvokeRepeating("test", 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger) { ps.Play(); trigger = false; }
        if(ps.isPlaying)
        {
            ps.GetParticles(particles);
            for (int i = 0; i < ps.particleCount; i++)
            {
                //particles[i].position = Vector3.Lerp(particles[i].position, transform.position, particles[i].velocity.magnitude);
                if(Vector3.Distance(particles[i].position, transform.position) > 0.1f)
                particles[i].position = Vector2.Lerp(particles[i].position, transform.position, (1 + (particles[i].remainingLifetime/particles[i].startLifetime * 3)) * Time.deltaTime);
                //particles[i].position = Vector3.Lerp(particles[i].position, transform.position, 0.1f * Time.deltaTime);
            }
            Debug.Log(particles.Length);
            ps.SetParticles(particles, ps.particleCount);
        }        
    }
    private void LateUpdate()
    {
        anim.SetFloat("X", xDir);
        anim.SetFloat("Y", yDir);
    }
    private void Fear(bool toggle)
    {
        anim.SetBool("Scared", toggle);
    }
    private void Dead(bool toggle)
    {
        anim.SetBool("Dead", toggle);
    }
    void test()
    {
        switch (testCase)
        {
            case testCases.left:
                xDir = -1;
                yDir = 0;
                break;
            case testCases.up:
                xDir = 0;
                yDir = 1;
                break;
            case testCases.upagain:
                xDir = 0;
                yDir = 1;
                break;
            case testCases.right:
                xDir = 1;
                yDir = 0;
                break;
            case testCases.rightagain:
                xDir = 1;
                yDir = 0;
                break;
            case testCases.down:
                xDir = 0;
                yDir = -1;
                break;
            case testCases.scared:
                Fear(true);
                break;
            case testCases.notscared:
                Fear(false);
                trigger = true;
                break;
            case testCases.scaredagain:
                Fear(true);
                break;
            case testCases.dead:
                Dead(true);
                break;
            case testCases.notdead:
                Dead(false);
                Fear(false);
                break;
            default:
                break;
        }
        testCase++;
        if (testCase == testCases.leftagain) testCase = testCases.left;
    }
    private enum testCases
    {
        left,
        up,
        right,
        down,
        scared,
        notscared,
        rightagain,
        upagain,
        scaredagain,
        dead,
        notdead,
        leftagain
    }
}
