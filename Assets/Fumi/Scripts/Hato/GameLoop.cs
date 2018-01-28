using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


// ゲームのループに関わるイベントを記述する予定
public class GameLoop : MonoBehaviour
{
    [SerializeField] float onPlayerDeadWaitSec = 2.0f;
    [SerializeField] bool enableAutoAdjustFreqArea = true;

    [SerializeField] float onClearWaitSec = 2.0f;
    [SerializeField] AudioClip clearClip;

    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        if (GameEventManager.Instance != null)
        {
            GameEventManager.Instance.OnPlayerDead += OnPlayerDead;
        }

        GameEventManager.Instance.OnClear += OnClear;
    }

    void OnDisable()
    {
        if (GameEventManager.Instance != null)
        {
            GameEventManager.Instance.OnPlayerDead -= OnPlayerDead;
        }
        if (GameEventManager.Instance != null)
        {
            GameEventManager.Instance.OnClear -= OnClear;
        }
    }

    // ゲームオーバーからGameやり直し。演出込み
    void OnPlayerDead()
    {
        if (enableAutoAdjustFreqArea)
        {
            InputVoice.Instance.SaveAvarageFreq();
        }
        StartCoroutine(PlayerDeadProgress());
    }

    IEnumerator PlayerDeadProgress()
    {
        yield return new WaitForSeconds(onPlayerDeadWaitSec);
        SceneManager.LoadScene("Game");
    }

    void OnClear()
    {
        StartCoroutine(ClearProgress());
    }

    IEnumerator ClearProgress()
    {
        audio.PlayOneShot(clearClip);

        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(1.0f);
        Time.timeScale = 1.0f;

        yield return new WaitForSeconds(onClearWaitSec);
        SceneManager.LoadScene("Title");
    }
}
