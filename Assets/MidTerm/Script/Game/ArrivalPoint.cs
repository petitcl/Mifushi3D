using UnityEngine;
using System.Collections;

public class ArrivalPoint : MonoBehaviour {

	//private Unity callbacks
	private	void	OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			GameLevel.Instance.EndGame();
		}
	}
}
