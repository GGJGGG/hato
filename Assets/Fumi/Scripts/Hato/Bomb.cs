using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomb : MonoBehaviour
{
    [SerializeField] ParticleSystem clearEffect;
    [SerializeField] ParticleSystem deadEffect;
    [SerializeField] Renderer bomb;

    Rigidbody rigid;
    bool goalHitted; //ゴールに当たったかどうか 

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (goalHitted) return;

        var isHitGoal = collision.gameObject.layer == LayerMask.NameToLayer("Goal");

        if (!isHitGoal)
        {
            Debug.Log("ゴール以外にバームクーヘンが当たった " + collision.gameObject.name);
            Dead();
        }
    }

    void OnEnable()
    {
        GameEventManager.Instance.OnClear += OnClear;
    }

    void OnDisable()
    {
        if (GameEventManager.Instance != null)
        {
            GameEventManager.Instance.OnClear -= OnClear;
        }
    }

    // ジョイントから切り離す
    public void Detach()
    {
        var joint = GetComponent<FixedJoint>();
        Destroy(joint);
        rigid.useGravity = true;
    }

    void OnClear()
    {
        goalHitted = true;
        rigid.useGravity = false;
        rigid.isKinematic = true;
        PlayClearEffect();
    }

    void PlayClearEffect(float rate = 1.0f)
    {
        var particleNum = (int)(100 * rate);
        clearEffect.Emit(particleNum);
    }

    void Dead()
    {
        EmitDeadParticle();
        Invoke("EmitDeadParticle", 0.2f);
        Invoke("EmitDeadParticle", 0.4f);

        bomb.enabled = false;
        var col = GetComponent<Collider>();
        col.enabled = false;
        GameEventManager.Instance.PlayerDead();

        var rigid = GetComponent<Rigidbody>();
        rigid.velocity = Vector3.zero;
        rigid.rotation = Quaternion.identity;
        rigid.freezeRotation = true;
    }

    void EmitDeadParticle()
    {
        deadEffect.Emit(8);
    }
}
