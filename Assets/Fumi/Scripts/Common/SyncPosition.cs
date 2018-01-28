using UnityEngine;
using System.Collections;

public class SyncPosition : MonoBehaviour
{
    public Transform _targetTransform;
    public Vector3 AddPosition;
    public Vector3 MulPosition;
    public float EffectiveRate = 1.0f;
	Transform _trans;

	void Start()
	{
		_trans = transform;
	}

	void LateUpdate ()
	{
		if(_targetTransform == null) return;

        var newPos = Vector3.Scale(MulPosition,_targetTransform.position)  + AddPosition;

        _trans.position = Vector3.Lerp(_trans.position, newPos, EffectiveRate);
	}
}