using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// このエリアにボムが当たるとゴールに届けるような感じに落とす
/// </summary>
public class GoalArea : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bomb")
        {
            Debug.Log("ゴールエリアにバームクーヘンが当たった");
            var bomb = other.gameObject.GetComponent<Bomb>();
            bomb.Detach();
        }
    }
}
