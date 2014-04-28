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
	public	Runity.FadeInOut	fadeInOut;


	//private Unity callbacks
	private	void	Start() {

		float fadeTime = this.fadeInOut.fadeTime;
		this.fadeInOut.gameObject.SetActive(true);
		this.fadeInOut.FadeIn();
		this.WaitForNSeconds(fadeTime, this.onStartAnimationDone);

	}

	//private Runity callbacks
	private	void	onStartAnimationDone() {
		GameLevel.Instance.StartGame();
	}

	//private 
}
