using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] Transform bomb;

    void Start()
    {
        GameEventManager.Instance.OnEnterGoalLine += () =>
        {
            var sync = GetComponent<SyncPosition>();
            sync.enabled = false;
        };

        GameEventManager.Instance.OnClear += () =>
        {
            var sync = GetComponent<SyncPosition>();
            sync.enabled = true;
            sync._targetTransform = bomb;
            sync.AddPosition = new Vector3(0, 0, -4);
            sync.MulPosition = Vector3.one;
            sync.EffectiveRate = 0.07f;
        };
    }
}
