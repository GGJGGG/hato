using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkParticle : MonoBehaviour {

    private GameObject ht;

    // Use this for initialization
    void Start () {
        ht = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {

    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player pl = ht.GetComponent<Player>();
            ParticlePlaying pp = ht.GetComponent<ParticlePlaying>();
            if (pl.faint == false)
            {
                pl.FaintCheck();
                pp.isPlaying = true;
            }
        }
    }
}
