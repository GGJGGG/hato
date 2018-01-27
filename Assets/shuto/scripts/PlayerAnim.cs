using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour {

    Animator _animator;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _animator.SetBool("faint", true);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            _animator.SetBool("faint", false);
        }
    }
}
