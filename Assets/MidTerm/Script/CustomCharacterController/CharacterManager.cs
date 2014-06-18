using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

	public Animator animator;
	public CharacterMotor motor {
		get; private set;
	}
	public CharacterInput input {
		get; private set;
	}

	// Use this for initialization
	void Start () {
		motor = this.GetComponent<CharacterMotor>();
		input = this.GetComponent<CharacterInput>();
	}

	// Update is called once per frame
	void Update () {
	}

	public void Move(float v, float h) {
		if (motor != null) {
			motor.Move(v, h);
		} else {
			Debug.LogWarning("CharacterMotor does not exist");
		}
	}

	public void Jump() {
		if (motor != null) {
			motor.Jump();
		} else {
			Debug.LogWarning("CharacterMotor does not exist");
		}
	}

	public void Boost(bool activate) {
		if (motor != null) {
			motor.Boost(activate);
		} else {
			Debug.LogWarning("CharacterMotor does not exist");
		}
	}

}
