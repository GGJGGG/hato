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

    float speed = 0.08f;

    public void OnSkipStory()
    {
        WaitForShout();
    }

    void OnUpdateVoiceInput(bool isMute, float rate, int freq, float power)
    {
        if (state != TitleState.WaitingForShout) return;
        if (isMute) return;

        InputVoice.Instance.SaveCenterFreq(freq);

        TransitionScene();
    }

    void Start()
    {
        shout.SetActive(false);
    }

    void Update()
    {
        if (state == TitleState.DisplayingStory)
        {
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        story.transform.Translate(0, 0, speed);
        if (logo != null)
        {
            if (logo.transform.position.z > 25 && !logo.GetComponent<FadeOut>().hasStarted)
            {
                logo.GetComponent<FadeOut>().StartFadeOut();
                speed = 0.02f;
            }
        }

        if (text != null)
        {
            if (text.transform.position.z > 20)
            {
                if (!text.GetComponent<FadeOut>().hasStarted)
                {
                    text.GetComponent<FadeOut>().StartFadeOut();
                }
                WaitForShout();
            }
        }
    }

    void WaitForShout()
    {
        if (state != TitleState.DisplayingStory) return;

        TitleEventManager.Instance.WaitForShout();

        state = TitleState.WaitingForShout;
        shout.SetActive(true);
        story.SetActive(false);

        InputVoice.Instance.OnUpdateVoiceInput += OnUpdateVoiceInput;
    }

    void TransitionScene()
    {
        state = TitleState.Transitioning;

        // TODO: 画面フェード？

        SceneManager.LoadScene("Game");
    }
}
