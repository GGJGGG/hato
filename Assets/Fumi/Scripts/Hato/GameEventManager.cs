using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : SingletonMonoBehaviour<GameEventManager>
{
    public event Action OnPlayerDead;
    public event Action OnClear;
    public event Action OnEnterLastArea;
    public event Action OnEnterGoalLine;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LastAreaLine")
        {
            if (OnEnterLastArea != null)
            {
                OnEnterLastArea();
            }
        }

        if (other.gameObject.tag == "GoalLine")
        {
            if (OnEnterGoalLine != null)
            {
                OnEnterGoalLine();
            }
        }
    }

    public void PlayerDead()
    {
        if (OnPlayerDead != null)
        {
            OnPlayerDead();
        }
    }

    public void GoalBomb()
    {
        if (OnClear != null)
        {
            OnClear();
        }
    }
}
