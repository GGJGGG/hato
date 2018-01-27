using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hato : MonoBehaviour {

    MeshRenderer mr;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private Vector3 hatoVelocity;
    [SerializeField]
    private float revivalLine;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float jet;
    private float revivalCount = 0;
    public static bool faint = false;

    // Use this for initialization
    void Start () {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update () {
        HatoMove();
        if (faint == true)
        {
            RevivalFaint();
        }
        if (faint == true)
        {
            mr.material.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
        }
    }

    void HatoMove()
    {
        hatoVelocity.x = jet;
        hatoVelocity.y -= gravity;
        if (Input.GetKey(KeyCode.W) && faint == false)
        {
            hatoVelocity.y += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && faint == false)
        {
            hatoVelocity.y -= speed * Time.deltaTime;
        }
        GetComponent<Rigidbody>().velocity = hatoVelocity;
    }

    public void FaintCheck ()
    {
        faint = true;
        Debug.Log("faint");
    }

    void RevivalFaint ()
    {
        revivalCount += 1 * Time.deltaTime;
        if(revivalCount >= revivalLine)
        {
            faint = false;
            revivalCount = 0;
            mr.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            Debug.Log("revival");
        }
    }
}
