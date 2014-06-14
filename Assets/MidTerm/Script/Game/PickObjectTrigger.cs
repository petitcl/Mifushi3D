using UnityEngine;
using System.Collections;

public class PickObjectTrigger : MonoBehaviour {

	GameObject		ToPick;

	private	void	OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<ColorCharacterController>().PickObject(this.ToPick);
		}
	}
}
