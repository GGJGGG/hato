using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkParticle : MonoBehaviour {

    private GameObject ht;
    //[SerializeField] GameObject sparkParticle;
    //[SerializeField] Transform[] sparkPoint;

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
            if (Player.faint == false)
            {
                Player.FaintCheck();
                ParticlePlaying.isPlaying = true;
                //foreach (Transform sparkPos in sparkPoint)
                //{
                //    GameObject spark = Instantiate(sparkParticle, sparkPos.position, transform.rotation) as GameObject;
                //    Destroy(spark, 1f);
                //}
            }
        }
    }
}
