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

    void Update()
    {
        transform.position += new Vector3(frontMoveSpeed, 0, 0) * Time.deltaTime * 60;
    }

    public void Boost(float addPower)
    {
        if (addPower == 0) return;

        var power = (addPower - 0.5f) * powerScale * Time.deltaTime * 60;
        transform.position += new Vector3(0, power, 0);
    }

    void Update()
    {
        DebugInput();
    }

    void DebugInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(frontMoveSpeed * 4, 0, 0) * Time.deltaTime * 60;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(frontMoveSpeed * -4, 0, 0) * Time.deltaTime * 60;
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
