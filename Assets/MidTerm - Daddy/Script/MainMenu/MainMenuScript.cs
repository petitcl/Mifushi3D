using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuScript : MonoBehaviour {

	public Selectable[] menuItemList;

	private int indexSelected = 0;

	public void Start() {
		menuItemList[indexSelected].Select();
	}

	public void RunSelection() {
		menuItemList[indexSelected].Run();
	}

	// Use this for initialization
	public void SelectUp () {
		indexSelected--;
		if(indexSelected < 0) indexSelected = menuItemList.Length - 1;
		UpdateMenu();
	}
	
	// Update is called once per frame
	public void SelectDown () {
		indexSelected++;
		if(indexSelected > menuItemList.Length - 1) indexSelected = 0;
		UpdateMenu();
	}

	public void UpdateMenu() {
		foreach (Selectable item in menuItemList) {
			item.UnSelect();
		}
		menuItemList[indexSelected].Select();
	}
}
