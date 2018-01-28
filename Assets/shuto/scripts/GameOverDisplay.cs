using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class GameOverDisplay : MonoBehaviour {

    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject blackBack;

    private void Start()
    {
        gameOver.SetActive(false);
        blackBack.SetActive(false);
    }

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

    void OnPlayerDead()
    {
        gameOver.SetActive(true);
        blackBack.SetActive(true);
    }
}
