using UnityEngine;
using System.Collections;

public class InputGameLevel : MonoBehaviour {

	private	void	Update() {
		if (Input.GetButtonUp("TogglePause")) {
			GameLevel.Instance.TogglePause();
		}
		if (Input.GetKey(KeyCode.Return)) {
			GameLevel.Instance.ReturnMainMenu();
		}
	}
}
