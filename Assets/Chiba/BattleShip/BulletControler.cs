using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShots : MonoBehaviour {
    public GameObject BULLET;
    public Transform muzzle;
	// Use this for initialization
	void Start () {
       // transform.Rotate(new Vector3(0, 1, 0),90);
	}
	
	// Update is called once per frame
	void Update () {
		//if()
        {
            GameObject bullets = GameObject.Instantiate(BULLET) as GameObject;
            BULLET.GetComponent<Bullet>().BulletStart();
        }
	}
}
