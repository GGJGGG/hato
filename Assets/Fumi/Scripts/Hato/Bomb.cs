using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] ParticleSystem clearEffect;

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
}
