﻿using UnityEngine;
using System.Collections;

//@script RequireComponent(CharacterMotor)
public class Character_Manager : MonoBehaviour {

	public static CharacterController CharacterControllerComponent;

	public static Character_Manager Instance;

	public float deadZone = 0.01f;

	public float Gravity = -9.81f;

	public float VerticalVelocity;

	public	bool	CanMove;

	private bool inputUpdated;

	public bool InputUpdated {
		get {
			return inputUpdated;
		}
	}

	// Use this for initialization
	private void Start() {
	
	}

	private void Awake() {
		Instance = this;

		Character_Manager.CharacterControllerComponent = GetComponent<CharacterController>();
		if (CharacterControllerComponent == null) {
			//Handle error when no CharacterController Component
		}
	}
	
	// Update is called once per frame
	private void FixedUpdate() {
		this.ControllerInput();
		if (this.CanMove) this.ActionInput();
		Animation_Manager.Instance.CurrentMotionState();
		Character_Motor.Instance.ControlledUpdate();
	}

	private void ControllerInput() {
		float v,h;
		if (this.CanMove) {
			v = Input.GetAxis("Vertical");
			h = Input.GetAxis("Horizontal");
		} else {
			v = 0.0f;
			h = 0.0f;
		}


		inputUpdated = false;

		this.VerticalVelocity = Character_Motor.Instance.MoveVector.y;
		Character_Motor.Instance.MoveVector = Vector3.zero;
		//change how the input is fetched to deal with "normal" 
		if ((v > deadZone || v < -deadZone) && !Character_Motor.Instance.IsSliding()) {
			Character_Motor.Instance.MoveVector.y = -v;
			inputUpdated = true;
		}
		if (h > deadZone || h < -deadZone) {
			Character_Motor.Instance.MoveVector.x = h;
			inputUpdated = true;
		}
	}

	private void ActionInput() {
		if (Input.GetButton("Jump")) {
			this.DelegateJump();
		}
	}

	public bool isJumping() {
		return Input.GetButton("Jump");
	}

	private void DelegateJump() {
		Character_Motor.Instance.Jump();
	}

	public void ResetSpeed() {
		//Character_Motor.Instance.MoveVector = Vector3.zero;
	}
}