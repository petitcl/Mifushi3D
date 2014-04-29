using UnityEngine;
using System.Collections;

public class DeadlyZone : MonoBehaviour {

	//private Unity callbacks
	private	void	OnTriggerEnter(Collider collider) {
		if (collider.tag == "Player") {
			GameLevel.Instance.onPlayerWalkedOnDeadlyZone(this);
		}
	}
}
