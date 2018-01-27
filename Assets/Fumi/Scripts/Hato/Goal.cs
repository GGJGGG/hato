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
            var bomb = collision.gameObject.GetComponent<Bomb>();
            bomb.Detach();
            bomb.PlayClearEffect();
            Time.timeScale = 0.2f;
            Invoke("ResetTimeScale", 0.2f);
        }
    }

    void ResetTimeScale()
    {
        Time.timeScale = 1;
    }
}
