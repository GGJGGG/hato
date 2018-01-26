using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPowerView : MonoBehaviour
{
    [SerializeField] Image fillImage;

    public void SetPower(float rate)
    {
        fillImage.fillAmount = rate;
    }
}
