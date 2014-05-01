using UnityEngine;
using System.Collections;

public class BasePickableObject : MonoBehaviour, IPickableObject {


	public	float	OnPickScale = 0.5f;

	//public methods
	#region IPickableObject implementation
	public void OnPick(GameObject picker) {



		this.transform.parent = picker.transform;

		ColorCharacterController ccc = picker.GetComponent<ColorCharacterController>();

		this.collider.enabled = false;
		this.transform.localScale *= this.OnPickScale;
		if (ccc == null) {
			this.transform.position = ccc.transform.position + ccc.transform.forward * 2.0f;
		} else {
			this.transform.position = ccc.PickObjectSpawn.position;
		}
	}
	public void OnDrop(GameObject picker) {
		throw new System.NotImplementedException ();
	}
	#endregion

	//private Unity callbacks
	private	void	Update() {
	
	}
}
