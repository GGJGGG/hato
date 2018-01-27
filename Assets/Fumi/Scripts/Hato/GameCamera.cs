using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    void Start()
    {
        GameEventManager.Instance.OnEnterGoalLine += () =>
        {
            var sync = GetComponent<SyncPosition>();
            sync.enabled = false;
        };
    }
}
