using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EWDamage : MonoBehaviour {

    private GameObject hato;

    // Use this for initialization
    void Start () {
        hato = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player p = hato.GetComponent<Player>();
            if (Player.faint == false)
            {
                p.FaintCheck();
            }
        }
    }
}
