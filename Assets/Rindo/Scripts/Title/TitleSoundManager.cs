using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgm;

    void Start()
    {
        bgm.Play();

        TitleEventManager.Instance.OnWaitingForShout += () =>
        {
            bgm.Stop();
        };
    }
}
