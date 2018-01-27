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
            bgm1.Stop();
            fill.Play();
            Invoke("PlayBgm2", fill.clip.length);
        };

        GameEventManager.Instance.OnEnterGoalLine += () =>
        {
            //TODO フェードアウトとかしたほうが良さそう
            bgm2.Stop();
        };

        GameEventManager.Instance.OnPlayerDead += () => 
        {
            bgm1.Stop();
            fill.Stop();
            bgm2.Stop();
        };
    }

    void PlayBgm2()
    {
        bgm2.Play();
    }
}
