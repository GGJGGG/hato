using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPowerView : MonoBehaviour
{
    [SerializeField] RectTransform rt;
    [SerializeField] Button button;
    [SerializeField] Text text;

    public void SetPower(float rate, int freq)
    {
        var noInput = rate == 0.0f;
        var anchor = new Vector2(0.5f, noInput ? 0.5f : rate);
        rt.anchorMin = anchor;
        rt.anchorMax = anchor;
        rt.anchoredPosition = new Vector3(8f, 0);
        button.interactable = !noInput;
        text.text = string.Format("{0}Hz", freq);
    }
}
