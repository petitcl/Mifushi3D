using UnityEngine;
using System.Collections;

public class ColorInputReader : MonoBehaviour {

	private	ColorCharacterController	gameController;

	private	void Awake () {
		this.gameController = GetComponent<ColorCharacterController>();
	}
	
	private void Update () {
		if (Input.GetButtonDown("Red")) {
			this.gameController.ChangeColor(GameLevel.GameColor.Red);
		} else if (Input.GetButtonDown("Green")) {
			this.gameController.ChangeColor(GameLevel.GameColor.Green);
		} else if (Input.GetButtonDown("Blue")) {
			this.gameController.ChangeColor(GameLevel.GameColor.Blue);
		} else if (Input.GetButtonDown("Pick")) {
			if (this.gameController.PickedObject != null) this.gameController.DropObject();
			else this.gameController.PickObject(); 
		}
	}
}
