using UnityEngine;
using System.Collections;

public class TextCheckPoint : MonoBehaviour {

	Animator animator;

	public void Start() {
		animator = this.GetComponent<Animator>();
		this.GetComponent<Renderer>().enabled = false;
	}

	void CheckPointActivated () {
		animator.SetBool("Activated", true);
		this.GetComponent<Renderer>().enabled = true;
	}

	void CheckPointStop () {
		this.GetComponent<Renderer>().enabled = false;
		animator.SetBool("Activated", false);
	}

}
