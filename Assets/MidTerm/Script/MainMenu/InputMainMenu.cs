using UnityEngine;
using System.Collections;

public class InputMainMenu : MonoBehaviour {

	MainMenuScript mainMenu;
	private float lastSelection;

	void Start() {
		mainMenu = this.GetComponent<MainMenuScript>();
	}

	// Update is called once per frame
	void Update () {
		if (mainMenu == null)
			return ;
		if (Time.time - lastSelection > 0.2f || Input.GetButtonDown("MenuUp") || Input.GetButtonDown("MenuDown")) {
			if (Input.GetButton("MenuUp")) {
				mainMenu.SelectUp();
			} else if (Input.GetButton("MenuDown")){
				mainMenu.SelectDown();
			}
			lastSelection = Time.time;
		}
		if (Input.GetKeyUp(KeyCode.Return)) {
			mainMenu.RunSelection();
		}

	}
}
