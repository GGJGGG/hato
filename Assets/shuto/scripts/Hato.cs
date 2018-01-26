using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hato : MonoBehaviour {

    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private Vector3 hatoVelocity;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        HatoMove();
    }

    void HatoMove()
    {
        hatoVelocity.x = 3;
        hatoVelocity.y -= 0.02f;
        if (Input.GetKey(KeyCode.W))
        {
            hatoVelocity.y += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            hatoVelocity.y -= speed * Time.deltaTime;
        }

        GetComponent<Rigidbody>().velocity = hatoVelocity;
    }

}
