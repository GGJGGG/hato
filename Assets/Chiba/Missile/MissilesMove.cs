using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilesMove : MonoBehaviour {
    public float MissileSpeed;
    
	// Update is called once per frame
	void Update () {
		
	}
   public void MisslesStart()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(MissileSpeed, 0, 0));
    }
}
