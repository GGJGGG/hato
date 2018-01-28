using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            Debug.Log("ゴールにバームクーヘンが当たった");
            GameEventManager.Instance.GoalBomb();
        }
    }
}
