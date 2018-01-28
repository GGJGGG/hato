using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlaying : MonoBehaviour {

    public bool isPlaying;
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

    public void PlayParticle()
    {
        isPlaying = true;
        var audio = particle.GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);
    }
}
