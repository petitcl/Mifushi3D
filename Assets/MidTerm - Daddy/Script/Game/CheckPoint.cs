using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public	int		order = 0;

	//private Unity callbacks
	private	void	OnTriggerEnter(Collider collider) {
		if (collider.tag == "Player") {
			GameLevel.Instance.onPlayerWalkedOnCheckPoint(this);
			this.BroadcastMessage("CheckPointActivated");
		}
	}
}
