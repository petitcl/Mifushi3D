using UnityEngine;
using System.Collections;

public class CharacterInput : MonoBehaviour {

	private CharacterAnimation anim;
	private CharacterMotor motor;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<CharacterAnimation>();
		motor = this.GetComponent<CharacterMotor>();
	}
	
	// Update is called once per frame
	void Update () {
		this.Move();
		this.Jump ();
		//motor.ControlledUpdate();
	}

	void Move() {
		float v,h;
		v = Input.GetAxis("Vertical");
		h = Input.GetAxis("Horizontal");
		
		if (anim != null) {
			anim.Move(v, h);
		} else {
			Debug.LogWarning("CharacterAnimation does not exist");
		}

		if (motor != null) {
			motor.Move(v, h);
		} else {
			Debug.LogWarning("CharacterMotor does not exist");
		}
	}

	void Jump() {
		if (Input.GetButtonDown("Jump")) {
			if (anim != null) {
				anim.Jump();
			} else {
				Debug.LogWarning("CharacterAnimation does not exist");
			}
			
			if (motor != null) {
				motor.Jump();
			} else {
				Debug.LogWarning("CharacterMotor does not exist");
			}

		}
	}
}
