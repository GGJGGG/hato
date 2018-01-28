using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomb : MonoBehaviour
{
    [SerializeField] ParticleSystem clearEffect;
    [SerializeField] ParticleSystem deadEffect;
    [SerializeField] Renderer bomb;

    bool goalHitted; //ゴールに当たったかどうか 

    void OnCollisionEnter(Collision collision)
    {
        if (goalHitted) return;

        var isHitGoal = collision.gameObject.layer == LayerMask.NameToLayer("Goal");

        if (isHitGoal)
        {
            goalHitted = true;
        }
        else
        {
            Debug.Log("ゴール以外にバームクーヘンが当たった " + collision.gameObject.name);
            Dead();
        }
    }

    // ジョイントから切り離す
    public void Detach()
    {
        var joint = GetComponent<FixedJoint>();
        Destroy(joint);
        var col = GetComponent<Collider>();
        col.enabled = false;
    }

    public void PlayClearEffect(float rate = 1.0f)
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
