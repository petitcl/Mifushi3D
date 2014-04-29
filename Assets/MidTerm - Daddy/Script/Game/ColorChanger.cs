using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

	//public attributes
	public	float	fadeTime = 0.5f;
	public	bool	sharedMaterial;
	public	bool	allMaterials;

	//private attributes
	private	float	startTime;
	private	Color	startColor;
	private	Color	endColor;
	

	//public methods
	public	void	FadeTo(Color end) {
		this.startColor = this.GetMaterial().color;
		this.endColor = end;
		this.startTime = Time.time;
		this.enabled = true;
	}

	//private Unity Callbacks
	private	void	Update() {
		float currTime = Time.time - this.startTime;
		float offset = currTime / this.fadeTime;
		Color nColor = Color.Lerp(this.startColor, this.endColor, offset);
		this.SetColor(nColor);
//		this.GetMaterial().color = nColor;
		if (nColor.Equals(this.enabled)) {
			this.enabled = false;
		}
	}

	//private methods
	private	Material	GetMaterial() {
		if (this.sharedMaterial) {
			return this.renderer.sharedMaterial;
		} else {
			return this.renderer.material;
		}
	}

	private	void		SetColor(Color ncolor) {
		Material[] materials = (this.sharedMaterial ? this.renderer.sharedMaterials : this.renderer.materials);
		if (this.allMaterials) {
			for (int i = 0; i < materials.Length; ++i) {
				materials[i].color = ncolor;
			}
		} else {
			materials[0].color = ncolor;
		}
	}
}
