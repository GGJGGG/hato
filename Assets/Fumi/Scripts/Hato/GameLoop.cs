using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


// ゲームのループに関わるイベントを記述する予定
public class GameLoop : MonoBehaviour
{
    [SerializeField] float onPlayerDeadWaitSec = 2.0f;
    [SerializeField] bool enableAutoAdjustFreqArea = true;

    void OnEnable()
    {
        if (GameEventManager.Instance != null)
        {
            GameEventManager.Instance.OnPlayerDead += OnPlayerDead;
        }
    }

    void OnDisable()
    {
        if (GameEventManager.Instance != null)
        {
            GameEventManager.Instance.OnPlayerDead -= OnPlayerDead;
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
}
