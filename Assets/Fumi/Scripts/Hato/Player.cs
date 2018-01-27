using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float powerScale = 1;
    [SerializeField] float frontMoveSpeed = 0.1f;

    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void Boost(float addPower)
    {
        if (addPower == 0) return;

        var power = (addPower - 0.5f) * powerScale * Time.deltaTime * 60;
        transform.position += new Vector3(0, power, 0);
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(frontMoveSpeed, 0, 0) * Time.fixedDeltaTime * 60;
    }

    void Update()
    {
        DebugInput();
    }

    void DebugInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            frontMoveSpeed += 0.1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            frontMoveSpeed -= 0.1f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Boost(1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Boost(0+Time.deltaTime);
        }
    }
}
