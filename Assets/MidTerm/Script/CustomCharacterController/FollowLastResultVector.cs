using UnityEngine;
using System.Collections;

public class FollowLastResultVector : MonoBehaviour {

	public CharacterMotor motor;
	public Vector3 scale;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 scaledResultVector = motor.LastResultVector;
		scaledResultVector.Scale(scale);
		this.transform.position = motor.transform.position + offset + scaledResultVector;
	}
}
