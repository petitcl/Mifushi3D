using UnityEngine;
using System.Collections;

public class EventNotifier : MonoBehaviour {

	//public attributes
	public	string			EventName;

	//private Unity methods
	private	void		OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			GameLevel.Instance.GameScene.ProcessEvent(this.EventName);
			GameObject.Destroy(this.gameObject);
		}
	}
}
