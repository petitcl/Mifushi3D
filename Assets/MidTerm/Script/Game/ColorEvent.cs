using UnityEngine;
using System.Collections;

public class ColorEvent : MonoBehaviour {

	//public types
	public	enum EventInputType {
		TriggerActivated
	}

	public	enum ResultType {
		ChangeColor,
		ToggleColor
	}

	//public attributes
	public	EventInputType	InputEvent;
	public	ResultType		Result;

	public	string			TriggerLabel;
	public	GameLevel.GameColor	Color1;
	public	GameLevel.GameColor	Color2;

	//public methods
	public void		TriggerEvent() {
		switch (this.Result) {
		case ResultType.ChangeColor:
			this.ChangeColor();
			break;
		default:
			break;
		}
	}

	//private methods
	private	void	ChangeColor() {
		SceneObject so = this.gameObject.GetComponent<SceneObject>();
		if (!so) return;
		so.SetColor(this.Color1);
	}

	//private Unity callbacks
	private	void	Start() {
		if (this.InputEvent == EventInputType.TriggerActivated) {
			Runity.Messenger<string>.AddListener("Player.WalkedOnTrigger", this.onPlayerWalkedOnTrigger);
		}
	}

	//private Runity callbacks
	private	void	onPlayerWalkedOnTrigger(string label) {
		if (!this.TriggerLabel.Equals(label)) return;
		if (!this.InputEvent.Equals(EventInputType.TriggerActivated)) return;
		this.TriggerEvent();
	}

}
