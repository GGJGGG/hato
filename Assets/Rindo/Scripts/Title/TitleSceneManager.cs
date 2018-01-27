using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TransitionScene();
        }
    }

    void TransitionScene()
    {
        // TODO: ストーリー演出？ あるいはGameではないシーンに遷移？
        SceneManager.LoadScene("Game");
    }
}
