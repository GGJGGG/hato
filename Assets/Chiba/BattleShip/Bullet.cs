using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private float Speed;
    [SerializeField]
	// Use this for initialization
	void Start () {
       // transform.Rotate(new Vector3(0, 1, 0), 90);
    }

    // Update is called once per frame
    public void BulletStart()
    {
        transform.position += new Vector3(Speed, 0, 0);

    }
}
