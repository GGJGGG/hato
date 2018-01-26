using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    private GameObject player;
    private Vector3 offset = Vector3.zero;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 newPosition = transform.position;
        newPosition.x = player.transform.position.x + offset.x;
        newPosition.z = player.transform.position.z + offset.z;
        transform.position = newPosition;
    }
}
