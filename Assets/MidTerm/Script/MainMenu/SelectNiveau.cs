using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectNiveau : MonoBehaviour {

	public TextMesh leftArrow;
	public TextMesh rightArrow;
	public List<GameObject> levels = new List<GameObject>();
	public List<string> level_names = new List<string>();
	public int currentLevel = 0;
	// Use this for initialization
	void Start () {
		UpdateCurrent();
	}
	
	// Update is called once per frame
	void Update () {
		int tmpCurrent = currentLevel;
		if (canLeft() && Input.GetButtonDown("MenuLeft")) {
			currentLevel--;
		}  
		else if (canRight() && Input.GetButtonDown("MenuRight")) {
			currentLevel++;
		}
		if (tmpCurrent != currentLevel) {
			UpdateCurrent();
		}
	}
	void UpdateCurrent() {
		foreach (GameObject l_object in levels) {
			l_object.SetActive(false);
		}
		levels[currentLevel].SetActive(true);
		if (leftArrow != null) {
			if (canLeft()) {
				leftArrow.color = Color.black;
			}
			else {
				leftArrow.color = Color.gray;
			}
		}
		if (rightArrow != null) {
			if (canRight()) {
				rightArrow.color = Color.black;
			}
			else {
				rightArrow.color = Color.gray;
			}
		}
	}
	public string getLevelName() {
		if (currentLevel < level_names.Count) {
			return level_names[currentLevel];
		}
		return "";
	}
	bool canLeft() {
		return currentLevel != 0;
	}
	bool canRight() {
		return currentLevel + 1 != levels.Count;
	}
}
