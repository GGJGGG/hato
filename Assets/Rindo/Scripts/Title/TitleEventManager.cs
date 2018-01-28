using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEventManager : SingletonMonoBehaviour<TitleEventManager>
{
    public event Action OnWaitingForShout;

    public event Action OnTransitioning;

    public event Action OnFadeOutCompleted;

    public void WaitForShout()
    {
        if (OnWaitingForShout != null)
        {
            OnWaitingForShout();
        }
    }

    public void StartTransition()
    {
        if (OnTransitioning != null)
        {
            OnTransitioning();
        }
    }

    public void FadeOutCompleted()
    {
        if (OnFadeOutCompleted != null)
        {
            OnFadeOutCompleted();
        }
    }
}
