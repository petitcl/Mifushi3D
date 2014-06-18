using UnityEngine;
using System.Collections;

public class SetCameraToCharacter : MonoBehaviour {
	
	public CustomSmoothFollow	camera;
	public GameObject			player;
	public Transform			cameraToCharacter;
	private Vector3				savedCamToTarget;
	public float				minimumHeight;
	private float				savedMinimumHeight;

	void OnTriggerEnter () {
		Debug.Log("SetCameraToCharacter.Enter");
		savedCamToTarget = camera.startCamToTarget;
		camera.startCamToTarget = cameraToCharacter.position - player.transform.position;
		savedMinimumHeight = camera.minimumHeight;
		camera.minimumHeight = minimumHeight;
	}

	void OnTriggerExit() {
		Debug.Log("SetCameraToCharacter.Exit");
		camera.startCamToTarget = savedCamToTarget;
		camera.minimumHeight = savedMinimumHeight;
	}
}
