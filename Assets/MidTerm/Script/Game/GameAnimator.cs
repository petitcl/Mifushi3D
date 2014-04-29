using UnityEngine;
using System.Collections;

public class GameAnimator : Runity.MonoBehaviourExt {

	//static attributes
	private	static	GameAnimator	_instance = null;
	public	static	GameAnimator	Instance {
		get {
//			if (GameLevel._instance == null) {
//				GameLevel._instance = new GameLevel();
//			}
			return GameAnimator._instance;
		}
	}

	//public attributes
	public	Runity.FadeInOut	FadeInOut;
	public	Runity.MaterialAnimator		MatAnimator;


	//private Unity callbacks
	private	void	Start() {

		Runity.Messenger<GameLevel.GameColor>.AddListener("Player.ChangeColor", this.onPlayerChangeColor);

		float fadeTime = this.FadeInOut.fadeTime;
		this.FadeInOut.gameObject.SetActive(true);
		this.FadeInOut.FadeIn();
		this.WaitForNSeconds(fadeTime, this.onStartAnimationDone);

	}

	//private Runity callbacks
	private	void	onStartAnimationDone() {
		GameLevel.Instance.StartGame();
	}

	//TODO : GameAnimator.PlayAnim(name, callback)
	private	void	onPlayerChangeColor(GameLevel.GameColor newColor) {
		switch (newColor) {
		case GameLevel.GameColor.Blue:
			this.MatAnimator.FadeTo("Player", GameLevel.Instance.Blue);
			break;
		case GameLevel.GameColor.Red:
			this.MatAnimator.FadeTo("Player", GameLevel.Instance.Red);
			break;
		case GameLevel.GameColor.Green:
			this.MatAnimator.FadeTo("Player", GameLevel.Instance.Green);
			break;
		default:
			break;
		}
	}
}
