using UnityEngine;
using System.Collections;

public class PlayerPhysic : MonoBehaviour {
	
	public	float	pushPower;
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
		if (hit.gameObject.rigidbody == null)
			return;
		if (body == null || body.isKinematic)
			return;
		
		if (hit.moveDirection.y < -0.3F)
			return;
		Debug.Log(42);
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		body.AddForce(pushDir * pushPower);
	}
}
