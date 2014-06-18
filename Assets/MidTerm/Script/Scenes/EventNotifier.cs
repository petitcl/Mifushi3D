using UnityEngine;
using System.Collections;

public class EventNotifier : MonoBehaviour {

	//public attributes
	public	string			EnterEventName;

	//private Unity methods
	private	void		OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			if (GameLevel.Instance.GameScene != null) GameLevel.Instance.GameScene.ProcessEvent(this.EnterEventName);
			GameObject.Destroy(this.gameObject);
		}
	}
}
