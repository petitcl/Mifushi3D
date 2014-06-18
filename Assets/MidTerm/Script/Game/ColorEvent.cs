using UnityEngine;
using System.Collections;

public class ColorEvent : MonoBehaviour {

	//public types
	public	enum EventInputType {
		TriggerActivated,
		ObjectDropped
	}

	public	enum ResultType {
		ChangeColor,
		ToggleColor,
		ActivatePlatform
	}

	//public attributes
	public	EventInputType	InputEvent;
	public	ResultType		Result;

	public	string				TriggerLabel;
	public	GameLevel.GameColor	Color1;
	public	GameLevel.GameColor	Color2;
	public	MovingPlatform		Platform;

	//public methods
	public void		TriggerEvent() {
		switch (this.Result) {
		case ResultType.ChangeColor:
			this.ChangeColor();
			break;
		case ResultType.ActivatePlatform:
			this.ActivatePlatform();
			break;
		default:
			break;
		}
	}

	//private methods
	private	void	ChangeColor() {
		ColorBlock so = this.gameObject.GetComponent<ColorBlock>();
		if (!so) return;
		so.SetColor(this.Color1);
	}

	private	void	ActivatePlatform() {
		this.Platform.enabled = true;
	}

	//private Unity callbacks
	private	void	Start() {
		if (this.InputEvent == EventInputType.TriggerActivated) {
			Runity.Messenger<string>.AddListener("Player.WalkedOnTrigger", this.onPlayerWalkedOnTrigger);
		}
		if (this.InputEvent == EventInputType.ObjectDropped) {
			Runity.Messenger<string>.AddListener("Player.DropObjectOnTrigger", this.onPlayerDropObjectOnTrigger);
		}
		//		
	}

	private	void	OnTriggerEnter() {

	}

	//private Runity callbacks
	private	void	onPlayerWalkedOnTrigger(string label) {
		if (!this.TriggerLabel.Equals(label)) return;
		if (!this.InputEvent.Equals(EventInputType.TriggerActivated)) return;
		this.TriggerEvent();
	}

	private	void	onPlayerDropObjectOnTrigger(string label) {
		if (!this.TriggerLabel.Equals(label)) return;
		if (!this.InputEvent.Equals(EventInputType.ObjectDropped)) return;
		this.TriggerEvent();
	}

}
