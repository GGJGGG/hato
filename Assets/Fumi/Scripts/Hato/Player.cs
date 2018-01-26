using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float powerScale = 10;
    [SerializeField] float frontMoveSpeed = 10;

    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigid.AddForce(frontMoveSpeed, 0, 0);
    }

    public void Boost(float addPower)
    {
        if (addPower == 0) return;

        var power = (addPower - 0.5f) * powerScale;
        transform.position += new Vector3(0, power, 0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            Boost(1);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            Boost(0+Time.deltaTime);
        }
    }
}
