using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControler : MonoBehaviour {
    public Rigidbody2D rb;//
    public float RizeDown;
    public float Speed;
   
	// Use this for initialization
	// Update is called once per frame
	void Update () {
        rb.velocity = new Vector3(Speed, 0,0);
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(Speed, RizeDown,0);//上方向に上昇
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity = new Vector3(Speed, -RizeDown,0);//下方向に下降
        }
	}
}
