using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoalLine")
        {
            var sync = GetComponent<SyncPosition>();
            sync.enabled = false;
        }
    }
}
