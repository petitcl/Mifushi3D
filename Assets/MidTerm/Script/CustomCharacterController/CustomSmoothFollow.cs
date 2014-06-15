using UnityEngine;
using System.Collections;

public class CustomSmoothFollow : MonoBehaviour {

	public Transform target;
	public float damping = 1;
	public bool x = true,y = true,z = true;
	public Vector3 startCamToTarget;
	public float minimumHeight;
	private Vector3 velocity;

	// Use this for initialization
	void Start () {
		startCamToTarget =  transform.position - target.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (target == null)
			return;

		Vector3 tmpTargetPosition = target.position + startCamToTarget;
		if (!x ) tmpTargetPosition.x = transform.position.x;
		if (!y || tmpTargetPosition.y < minimumHeight) tmpTargetPosition.y = transform.position.y;
		if (!z) tmpTargetPosition.z = transform.position.z;
		transform.position = Vector3.SmoothDamp(transform.position, tmpTargetPosition, ref velocity, damping);
	}

	public void RotateArroundTarget(Vector3 axis, float angle) {
		startCamToTarget = Quaternion.AngleAxis(angle, axis) * startCamToTarget;
	}
	
}
