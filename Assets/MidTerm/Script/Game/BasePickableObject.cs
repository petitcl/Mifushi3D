using UnityEngine;
using System.Collections;

public class BasePickableObject : MonoBehaviour, IPickableObject {

	//public attributes
	public	float	OnPickScale = 0.5f;

	//private attributes
	private	Transform		oldParent;

	//public methods
	#region IPickableObject implementation
	
	public bool Pickable { get; set; }
	public bool Dropable { get; set; }

	public void OnPick(GameObject picker) {

		this.oldParent = this.transform.parent;
		this.transform.parent = picker.transform;

		ColorCharacterController ccc = picker.GetComponent<ColorCharacterController>();

		this.collider.enabled = false;
		this.rigidbody.isKinematic = true;
		this.transform.localScale *= this.OnPickScale;
		if (ccc == null) {
			this.transform.position = picker.transform.position + picker.transform.forward * 2.0f;
		} else {
			this.transform.position = ccc.PickObjectSpawn.position;
		}
		this.Pickable = false;
		this.Dropable = true;
	}


	public void OnDrop(GameObject picker) {
		this.transform.parent = this.oldParent;

		float scale = this.OnPickScale;
		if (scale == 0.0f) scale = 1.0f;
		this.transform.localScale /= this.OnPickScale;
		this.collider.enabled = true;
		this.transform.position = picker.transform.position - picker.transform.up * 2.0f + picker.transform.forward;
		this.rigidbody.isKinematic = false;
		
		this.Pickable = true;
		this.Dropable = false;
	}

	#endregion

	//private Unity callbacks
	private	void	Start() {
		this.Pickable = true;
		this.Dropable = false;
	}
}
