using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

	//public attributes

	public	GameObject		PauseScreen;
	public	GameObject		FinishScreen;

	//private attributes

	//private methods
	private	void		BeforeStartGUI() {

	}

	private	void		RunningGUI() {
		this.DrawTimer();
	}

	private	void		PausedGUI() {
		this.DrawTimer();
	}

	private	void		FinishedGUI() {
		
	}

	private	void		DrawTimer() {
		GUI.Label(new Rect(20, 20, 100, 20), "Time : ");
		GUI.Label(new Rect(130, 20, 100, 20), GameLevel.Instance.TimeSinceStart.ToString());
	}

	//private Unity callbacks
	private	void		Start() {
		Runity.Messenger.AddListener("Game.Pause", this.onGamePaused);
		Runity.Messenger.AddListener("Game.Resume", this.onGameResumed);
		Runity.Messenger.AddListener("Game.End", this.onGameFinished);
	}

	private void		OnGUI() {

		if (!GameLevel.Instance.Started) {
			this.BeforeStartGUI();
		} else if (GameLevel.Instance.Running) {
			this.RunningGUI();
		} else if (GameLevel.Instance.Paused) {
			this.PausedGUI();
		} else if (GameLevel.Instance.Finished) {
			this.FinishedGUI();
		}

	}

	//private Runity callbacks
	private void	onGamePaused() {
		this.PauseScreen.SetActive(true);
	}

	private void	onGameResumed() {
		this.PauseScreen.SetActive(false);
	}

	private void	onGameFinished() {
		this.FinishScreen.SetActive(true);
	}
}
