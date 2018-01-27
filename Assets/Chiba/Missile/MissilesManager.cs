using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilesManager : MonoBehaviour
{
    public GameObject Missle;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
    }

      void OnBecameVisible()

      {
        Missle.GetComponent<MissilesMove>().MisslesStart();

      }

}
