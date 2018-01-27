using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    enum TitleState
    {
        DisplayingStory, WaitingForShout, Transitioning
    }

    GameObject logo;

    GameObject story;

    GameObject text;

    TitleState state = TitleState.DisplayingStory;

    public void OnSkipStory()
    {
        if (state != TitleState.DisplayingStory)
        {
            return;
        }

        // TODO: 叫べ！ まで行く
    }

    void Start()
    {
        logo = GameObject.Find("Logo");
        text = GameObject.Find("Text");
        story = GameObject.Find("Story");
    }

    void Update()
    {
        switch (state)
        {
            case TitleState.DisplayingStory:
                UpdateDisplay();
                break;
            case TitleState.WaitingForShout:
                break;
            case TitleState.Transitioning:
                break;
        }
    }

    void UpdateDisplay()
    {
        story.transform.Translate(0, 0, 0.05f);

        if (story.transform.position.z > 30)
        {
            // TODO: フェードアウト処理
        }

        // TODO: とりあえずの処理なので正常にシーン遷移のロジック組む
        if (story.transform.position.z > 60)
        {
            TransitionScene();
        }
    }

    void TransitionScene()
    {
        SceneManager.LoadScene("Game");
    }
}
