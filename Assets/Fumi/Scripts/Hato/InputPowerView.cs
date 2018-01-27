using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPowerView : MonoBehaviour
{
    [SerializeField] RectTransform rt;
    [SerializeField] Button button;
    [SerializeField] Text text;

    [SerializeField] Text lowText;
    [SerializeField] Text centerText;
    [SerializeField] Text highText;

    void OnEnable()
    {
        if (InputVoice.Instance != null)
        {
            InputVoice.Instance.OnUpdateVoiceInput += OnUpdateVoiceInput;
            InputVoice.Instance.OnUpdateCenterFreq += OnUpdateCenterFreq;
        }
    }

    void OnDisable()
    {
        if (InputVoice.Instance != null)
        {
            InputVoice.Instance.OnUpdateVoiceInput -= OnUpdateVoiceInput;
            InputVoice.Instance.OnUpdateCenterFreq -= OnUpdateCenterFreq;
        }
    }

    void OnUpdateVoiceInput(bool isMute, float rate, int freq, float power)
    {
        var anchor = new Vector2(0.5f, isMute ? 0.5f : rate);
        rt.anchorMin = anchor;
        rt.anchorMax = anchor;
        rt.anchoredPosition = new Vector3(8f, 0);
        button.interactable = !isMute;
        text.text = ToFreqString(freq);
    }

    void OnUpdateCenterFreq(int lowFreq, int centerFreq, int highFreq)
    {
        lowText.text = ToFreqString(lowFreq);
        centerText.text = ToFreqString(centerFreq);
        highText.text = ToFreqString(highFreq);
    }

    string ToFreqString(int freq)
    {
        return freq == 0 ? "" : string.Format("{0}Hz", freq);
    }
}
