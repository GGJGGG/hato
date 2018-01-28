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

    [SerializeField] GameObject logo;

    [SerializeField] GameObject story;

    [SerializeField] GameObject text;

    [SerializeField] GameObject shout;

    TitleState state = TitleState.DisplayingStory;

    public void OnSkipStory()
    {
        if (state != TitleState.DisplayingStory)
        {
            return;
        }

        state = TitleState.WaitingForShout;
        shout.SetActive(true);
        story.SetActive(false);
    }

    void Start()
    {
        shout.SetActive(false);
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

        if (story.transform.position.z > 25)
        {
            if (logo != null && !logo.GetComponent<FadeOut>().hasStarted)
            {
                logo.GetComponent<FadeOut>().StartFadeOut();
            }
        }

        if (story.transform.position.z > 60)
        {
            if (text != null && !text.GetComponent<FadeOut>().hasStarted)
            {
                text.GetComponent<FadeOut>().StartFadeOut();
            }
            TransitionScene();
        }
    }

    void TransitionScene()
    {
        SceneManager.LoadScene("Game");
    }
}
