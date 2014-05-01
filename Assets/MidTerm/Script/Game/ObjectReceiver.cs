using UnityEngine;
using System.Collections;

public class ObjectReceiver : MonoBehaviour {

	//public attributes
	public	Transform		ObjectDropPosition;
	public	string			Label;

	//private Unity callbacks
	private	void		OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			ColorCharacterController ccc = other.gameObject.GetComponent<ColorCharacterController>();
			if (ccc == null) return;
			if (ccc.PickedObject == null) return;
			GameObject pobj = ccc.PickedObject;
			if (pobj.GetComponent<BasePickableObject>() == null) return;
			ccc.DropObject();
			pobj.transform.position = this.ObjectDropPosition.position;
			Runity.Messenger<string>.Broadcast("Player.DropObjectOnTrigger", this.Label, Runity.MessengerMode.DONT_REQUIRE_LISTENER);
		}
	}
}
