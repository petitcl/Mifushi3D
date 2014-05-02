using UnityEngine;
using System.Collections;

public class TutorialButton : Selectable {
	
	override public void Run() {
		Application.LoadLevel("TutorialLevel");
	}

}
