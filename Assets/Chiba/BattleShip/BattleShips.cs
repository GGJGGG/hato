using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShips : MonoBehaviour
{
    public GameObject BULLET;
    public GameObject BulletPosition;
    [SerializeField]
    private float count;
    [SerializeField]
    private float Interval;
    [SerializeField]

  
    // Use this for initialization
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BattleShipsStart()
    {
        Instantiate(BULLET,BulletPosition.transform.position,BulletPosition.transform.rotation);
    }
}
