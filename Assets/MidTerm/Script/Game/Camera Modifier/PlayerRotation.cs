using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour {

	public GameObject	player;
	public CustomSmoothFollow	camera;
	public float		rotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter () {
		player.transform.Rotate(Vector3.forward, rotation);
		camera.RotateArroundTarget(Vector3.up, rotation);
	}

	void OnTriggerExit() {
		player.transform.Rotate(Vector3.forward, -rotation);
		camera.RotateArroundTarget(Vector3.up, -rotation);
	}

}
