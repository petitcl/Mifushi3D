using UnityEngine;
using System.Collections;

public class RotateArround : MonoBehaviour {

	public Transform pivotPoint;
	public float rotationSpeed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(pivotPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
	}
}
