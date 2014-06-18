using UnityEngine;
using System.Collections;

public class ArrivalPoint : MonoBehaviour {

	private float enterTime = 0;

	//private Unity callbacks
	private	void	OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			enterTime = Time.time;
		}
	}

	private void Update() {
		if (enterTime != 0 && Time.time - enterTime > 0.3) {
			GameLevel.Instance.EndGame();
		}
	}
}
