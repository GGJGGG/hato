using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticlePlay : MonoBehaviour {

    [SerializeField] ParticleSystem particle;

    void OnCollisionEnter(GameObject other)
    {
        Debug.Log("hithit");
        if (other.tag == "Player")
        {
            particle.Play(true);
        }
    }
}
