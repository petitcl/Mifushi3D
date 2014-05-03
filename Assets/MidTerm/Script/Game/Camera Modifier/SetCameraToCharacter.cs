using UnityEngine;
using System.Collections;

public class SetCameraToCharacter : MonoBehaviour {
	
	public CustomSmoothFollow	camera;
	public GameObject			player;
	public Transform			cameraToCharacter;
	private Vector3				savedCamToTarget;
	public float				minimumHeight;
	private float				savedMinimumHeight;

	// Update is called once per frame
	void OnTriggerEnter () {
		Debug.Log ("OnTriggerEnter start" + camera.startCamToTarget);
		savedCamToTarget = camera.startCamToTarget;
		camera.startCamToTarget = cameraToCharacter.position - player.transform.position;
		savedMinimumHeight = camera.minimumHeight;
		camera.minimumHeight = minimumHeight;
		Debug.Log ("OnTriggerEnter end" + camera.startCamToTarget);
	}

	void OnTriggerExit() {
		Debug.Log ("OnTriggerExit");
		camera.startCamToTarget = savedCamToTarget;
		camera.minimumHeight = savedMinimumHeight;
	}
}
