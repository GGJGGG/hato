using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{

    Animator _animator;
    [SerializeField] float risingSpeed;
    [SerializeField] float normalSpeed;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            _animator.speed = risingSpeed;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            _animator.speed = normalSpeed;
        }
    }
    public void FiantAnimOn()
    {
        _animator.SetBool("faint", true);
    }
    public void FiantAnimOff()
    {
        _animator.SetBool("faint", false);
    }
    public void RisingAnim()
    {
        _animator.speed = risingSpeed;
    }
    public void NormalAnim()
    { 
        _animator.speed = normalSpeed;
    }
}
