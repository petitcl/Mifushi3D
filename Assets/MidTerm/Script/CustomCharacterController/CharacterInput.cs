using UnityEngine;
using System.Collections;

public class CharacterInput : MonoBehaviour {

	private CharacterManager manager;


	// Use this for initialization
	void Start () {
		manager = this.GetComponent<CharacterManager>();
	}

	// Update is called once per frame
	void Update () {
		this.Move();
		this.Jump ();
		//this.Boost();
	}

	void Move() {
		float v,h;
		v = Input.GetAxis("Vertical");
		h = Input.GetAxis("Horizontal");
		manager.Move(v, h);
	}


	void Jump() {
		if (Input.GetButtonDown("Jump")) {
			manager.Jump();
		}
	}
	
	void Boost() {
		if (Input.GetButton("Boost")) {
			manager.Boost(true);
		} else {
			manager.Boost(false);
		}
	}
}
