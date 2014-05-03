using UnityEngine;
using System.Collections;

public class SetCameraToCharacter : MonoBehaviour {
	
	public CustomSmoothFollow	camera;
	public GameObject			player;
	public Transform			cameraToCharacter;
	private Vector3				savedCamToTarget;

	// Update is called once per frame
	void OnTriggerEnter () {
		savedCamToTarget = camera.startCamToTarget;
		camera.startCamToTarget = cameraToCharacter.position - player.transform.position;
	}

	void OnTriggerExit() {
		camera.startCamToTarget = savedCamToTarget;
	}
}
