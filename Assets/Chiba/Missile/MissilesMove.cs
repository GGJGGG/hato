using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilesMove : MonoBehaviour {
    public float MissileSpeed;
    [SerializeField] ParticleSystem hitParticle;

    public void MisslesStart()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(MissileSpeed, 0, 0));
    }

    void OnCollisionEnter(Collision collision)
    {
        //hitParticle.Emit(40);
        hitParticle.Play(true);
        GetComponent<MeshRenderer>().enabled = false;
        var rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
        var col = GetComponent<Collider>();
        col.enabled = false;

        GetComponent<AudioSource>().Play();
    }
}
