using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float powerScale = 1;
    [SerializeField] float frontMoveSpeed = 0.1f;
    [SerializeField] float revivalLine;
    [SerializeField] float revivalCount = 0;


    public static bool faint = false;

    MeshRenderer mr;

    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
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

        // 地面に反射した反動で、物理挙動的に上向きなどに進んでいたら力を徐々に打ち消す
        if (rigid.velocity.y > 0)
        {
            rigid.velocity *= 0.8f;
        }
    }

    void Update()
    {
        if (faint == false)
        {
            DebugInput();
        }
        else if (faint == true)
        {
            RevivalFaint();
        }
    }

    void DebugInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            frontMoveSpeed += 0.01f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            frontMoveSpeed -= 0.01f;
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

    public void FaintCheck()
    {
        faint = true;
    }

    void RevivalFaint()
    {
        revivalCount += 1 * Time.deltaTime;
        mr.material.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
        if (revivalCount >= revivalLine)
        {
            faint = false;
            revivalCount = 0;
            mr.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
}
