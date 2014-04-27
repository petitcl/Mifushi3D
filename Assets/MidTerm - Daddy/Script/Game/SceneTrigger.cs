using UnityEngine;
using System.Collections;

public class SceneTrigger : MonoBehaviour {

	public	string	Label;

	//unity callbacks
	private	void	OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			GameLevel.Instance.onPlayerWalkedOnTrigger(this);
		}
	}
}
