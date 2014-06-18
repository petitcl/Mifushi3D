using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour {

	public bool resetOnExit = true;
	public GameObject	player;
	public CustomSmoothFollow	camera;
	public float		rotation;
	private bool		once = false;
	// Use this for initialization
	void Start () {
		once = false;
	}
	
	// Update is called once per frame
	void OnTriggerEnter () {
		if (!once) {
			player.transform.Rotate(Vector3.up, rotation);
			camera.RotateArroundTarget(Vector3.up, rotation);
			if (!resetOnExit) {
				once = true;
			}
		}
	}

	void OnTriggerExit() {
		if (resetOnExit) {
			player.transform.Rotate(Vector3.up, -rotation);
			camera.RotateArroundTarget(Vector3.up, -rotation);
		}
	}

}
