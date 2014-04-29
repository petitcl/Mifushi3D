using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public	int		order = 0;

	//private Unity callbacks
	private	void	Start() {
		Runity.Messenger<string>.AddListener("Player.WalkedOnCheckPoint", this.onCheckPointActivated);
	}

	private	void	OnTriggerEnter(Collider collider) {
		if (collider.tag == "Player") {
			GameLevel.Instance.onPlayerWalkedOnCheckPoint(this);
		}
	}

	//private Runity callbacks
	private	void	onCheckPointActivated(string cname) {
		if (this.name == cname) {
			this.BroadcastMessage("CheckPointActivated");
		}
	}

}
