using UnityEngine;
using System.Collections;

public class PlayButton : Selectable {
	
	override public void Run() {
		Application.LoadLevel("ChallengingLevel");
	}

}
