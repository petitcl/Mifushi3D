using UnityEngine;
using System.Collections;

public class SceneTrigger : MonoBehaviour {

	//public attributes
	public	string	Label;
	public	bool	triggerOnce;

	//private attributes
	private	bool	triggered = false;

	//public methods
	public	bool	CanBeTriggered() {
		if (this.triggerOnce && this.triggered) return false;
		else return true;
	}

	public	bool	Trigger() {
		if (!this.CanBeTriggered()) return false;

		this.triggered = true;
		//this.BroadcastMessage("Triggered");

		return true;
	}

	//unity callbacks
	private	void	OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			GameLevel.Instance.onPlayerWalkedOnTrigger(this);
		}
	}
}
