using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


// ゲームのループに関わるイベントを記述する予定
public class GameLoop : MonoBehaviour
{
    [SerializeField] float onPlayerDeadWaitSec = 2.0f;

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
        StartCoroutine(PlayerDeadProgress());
    }

    IEnumerator PlayerDeadProgress()
    {
        yield return new WaitForSeconds(onPlayerDeadWaitSec);
        SceneManager.LoadScene("Game");
    }
}
