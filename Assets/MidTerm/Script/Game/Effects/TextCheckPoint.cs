using UnityEngine;
using System.Collections;

public class TextCheckPoint : MonoBehaviour {

	Animator animator;
	public	GameObject		Particles;

	public void Start() {
		animator = this.GetComponent<Animator>();
		this.GetComponent<Renderer>().enabled = false;
	}

	void CheckPointActivated () {
		animator.SetBool("Activated", true);
		this.GetComponent<Renderer>().enabled = true;

		GameObject.Instantiate(this.Particles, this.transform.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
	}

	void CheckPointStop () {
		this.GetComponent<Renderer>().enabled = false;
		animator.SetBool("Activated", false);
	}

}
