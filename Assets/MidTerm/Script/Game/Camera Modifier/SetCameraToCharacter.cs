using UnityEngine;
using System.Collections;

public class SetCameraToCharacter : MonoBehaviour {
	
	public CustomSmoothFollow	camera;
	public GameObject			player;
	public Transform			cameraToCharacter;
	private Vector3				savedCamToTarget;
	public float				minimumHeight;
	private float				savedMinimumHeight;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {

			savedCamToTarget = camera.startCamToTarget;
			camera.startCamToTarget = cameraToCharacter.position - player.transform.position;
			savedMinimumHeight = camera.minimumHeight;
			camera.minimumHeight = minimumHeight;
			Debug.Log("SetCameraToCharacter.Enter");
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			
			camera.startCamToTarget = savedCamToTarget;
			camera.minimumHeight = savedMinimumHeight;
			Debug.Log("SetCameraToCharacter.Exit");
		}
	}
}
