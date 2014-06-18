using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {

	public Animator animator;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Move(float v, float h) {
		//Debug.Log("CharacterAnimation.Move(" + v + ", " + h + ")");
	}

	public void Jump() {
		animator.SetBool("IsJumping", true);
	}

}
