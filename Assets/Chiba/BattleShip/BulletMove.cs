using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
   [SerializeField]
    private float BulletSpeed;
   [SerializeField]
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().AddForce(-BulletSpeed,BulletSpeed , 0);
    }
}
