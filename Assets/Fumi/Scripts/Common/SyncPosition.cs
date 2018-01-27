using UnityEngine;
using System.Collections;

public class SyncPosition : MonoBehaviour
{
    public Transform _targetTransform;
    public Vector3 AddPosition;
    public Vector3 MulPosition;
	Transform _trans;

	void Start()
	{
		_trans = transform;
	}

	void LateUpdate ()
	{
		if(_targetTransform == null) return;

        _trans.position = Vector3.Scale(MulPosition,_targetTransform.position)  + AddPosition;
	}
}