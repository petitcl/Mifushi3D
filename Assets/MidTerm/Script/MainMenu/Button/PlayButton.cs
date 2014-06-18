using UnityEngine;
using System.Collections;

public class PlayButton : Selectable {
	
	override public void Run() {
		Debug.Log("LoadLevel");
		GameObject levels = GameObject.Find("levels");
		if (!levels) {
			return ;
		}
		Debug.Log("LoadLevel");
		SelectNiveau selector = levels.GetComponent<SelectNiveau>();
		if (!selector) {
			return ;
		}
		Debug.Log("LoadLevel");
		string level_name = selector.getLevelName();
		if (level_name == "") {
			return ;
		}
		Debug.Log("LoadLevel");
		Application.LoadLevel(level_name);
	}
}
