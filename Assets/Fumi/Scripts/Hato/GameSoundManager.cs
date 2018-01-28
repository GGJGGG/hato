using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgm1;
    [SerializeField] AudioSource fill;
    [SerializeField] AudioSource bgm2;

    void Start()
    {
        // TODO 移動開始してから音鳴らしたほうがいいかも
        bgm1.Play();

        GameEventManager.Instance.OnEnterLastArea += () =>
        {
            // TODO 綺麗につなぐ
            StartCoroutine(bgm1.Fade(0, 0.5f));
            fill.Play();
            Invoke("PlayBgm2", fill.clip.length - 0.05f);
        };

        GameEventManager.Instance.OnEnterGoalLine += () =>
        {
            //TODO フェードアウトとかしたほうが良さそう
            fill.Stop();
            StartCoroutine(bgm2.Fade(0, 1.0f));
        };

        GameEventManager.Instance.OnPlayerDead += () =>
        {
            StartCoroutine(bgm1.Fade(0, 0.1f));
            StartCoroutine(fill.Fade(0, 0.1f));
            StartCoroutine(bgm2.Fade(0, 0.1f));
        };
    }

    void PlayBgm2()
    {
        bgm2.Play();
    }
}
