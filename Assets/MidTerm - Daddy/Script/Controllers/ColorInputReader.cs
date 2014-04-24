using UnityEngine;
using System.Collections;

public class ColorInputReader : MonoBehaviour {

	private	ColorCharacterController	gameController;

	private	void Awake () {
		this.gameController = GetComponent<ColorCharacterController>();
	}
	
	private void Update () {
		if (Input.GetButton("Red")) {
			this.gameController.ChangeColor(GameLevel.GameColor.Red);
		} else if (Input.GetButton("Green")) {
			this.gameController.ChangeColor(GameLevel.GameColor.Green);
		} else if (Input.GetButton("Blue")) {
			this.gameController.ChangeColor(GameLevel.GameColor.Blue);
		} else if (Input.GetButton("Default")) {
			this.gameController.ChangeColor(GameLevel.GameColor.Default);
		}
	}
}
