using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEventManager : SingletonMonoBehaviour<TitleEventManager>
{
    public event Action OnWaitingForShout;

    public void WaitForShout()
    {
        if (OnWaitingForShout != null)
        {
            OnWaitingForShout();
        }
    }
}
