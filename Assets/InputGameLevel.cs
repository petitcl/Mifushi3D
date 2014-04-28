using UnityEngine;
using System.Collections;

public class InputGameLevel : MonoBehaviour {

	GameLevel gameLevel;

	// Use this for initialization
	void Start () {
		gameLevel = this.GetComponent<GameLevel>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp("TogglePause")) {
			gameLevel.TogglePause();
		}
		if (Input.GetKey(KeyCode.Return)) {
			gameLevel.ReturnMainMenu();
		}
	}
}
