using UnityEngine;
using System.Collections;

abstract public class Selectable : MonoBehaviour {

	public GameObject map;
	public GameObject icon;
	private Color selectedColor;
	private TextMesh textMesh;

	public void Start() {
		textMesh = this.GetComponent<TextMesh>();
		selectedColor = textMesh.color;
		textMesh.color = Color.white;
	}

	public void Select () {
		if (map != null) map.SetActive(true);
		if (icon != null) icon.SetActive(true);
		textMesh.color = selectedColor;
	}

	public void UnSelect() {
		if (map != null) map.SetActive(false);
		if (icon != null) icon.SetActive(false);
		textMesh.color = Color.white;
	}

	abstract public void Run();

}
