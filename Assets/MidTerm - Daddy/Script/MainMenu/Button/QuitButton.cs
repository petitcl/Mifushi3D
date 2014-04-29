using UnityEngine;
using System.Collections;

public class QuitButton : Selectable {
	
	override public void Run() {
		Application.Quit();
	}

}
