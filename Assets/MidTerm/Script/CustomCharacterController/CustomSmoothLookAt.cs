using UnityEngine;
using System.Collections;

public class CustomSmoothLookAt : MonoBehaviour {

	public Transform target;
	public bool x = true,y = true,z = true;

	// Update is called once per frame
	void LateUpdate () {
		if(target == null)
			return;

		Debug.DrawLine(target.position, transform.position, Color.yellow);
		//transform.LookAt(target.position);
		Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
		Vector3 euler = new Vector3();
		euler.x = x ? rotation.eulerAngles.x : transform.rotation.eulerAngles.x;
		euler.y = y ? rotation.eulerAngles.y : transform.rotation.eulerAngles.y;
		euler.z = z ? rotation.eulerAngles.z : transform.rotation.eulerAngles.z;
		rotation = Quaternion.Euler(euler);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
	}
}
