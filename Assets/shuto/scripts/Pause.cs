using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 1.0f;
        }
    }
}
