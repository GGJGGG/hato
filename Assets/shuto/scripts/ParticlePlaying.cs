using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlaying : MonoBehaviour {

    public static bool isPlaying;
    [SerializeField] ParticleSystem particle;

    private void Start()
    {
        isPlaying = false;
    }

    public void Update()
    {
        if (isPlaying == true)
        {
            particle.Play(true);
        }
        else
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
