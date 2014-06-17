using UnityEngine;
using System.Collections;

public class DeadlyZone : MonoBehaviour {

	public	bool		InsideBlock = false;

	//private Unity callbacks
	private	void	OnTriggerEnter(Collider collider) {
		if (collider.tag == "Player") {
			Debug.Log(collider.gameObject.name);
			GameLevel.Instance.onPlayerWalkedOnDeadlyZone(this);
		}
	}
}
