using UnityEngine;
using System.Collections;

public class MovingPlatformTrigger : MonoBehaviour {

	//private Unity callbacks
	private	void	OnTriggerStay(Collider other) {
		other.transform.parent = this.gameObject.transform.parent;
	}

	private	void	OnTriggerExit(Collider other) {
		if (other.transform.parent == this.gameObject.transform.parent) {
			other.transform.parent = null;
		}
	}
}
