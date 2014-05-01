using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

	//static attributes
	private	static	HUDManager	_instance = null;
	public	static	HUDManager	Instance {
		get {
			return HUDManager._instance;
		}
	}

	//public attributes
	public	float			ToolTipDuration = 2.0f;


	public	GameObject		PauseScreen;
	public	GameObject		FinishScreen;
	public	Camera			HUDCamera;
	public	Transform		HUDPopupPosition;


	//public methods
	public	void		DrawTooltip(GameObject tooltip) {
		this.DrawTooltip(tooltip, 2.0f);
	}

	public	void		DrawTooltip(GameObject tooltip, float duration) {
		this.DrawInfiniteTooltip(tooltip);
		this.StartCoroutine(this.DrawTooltipCoroutine(tooltip, duration));
	}

	public	void		DrawInfiniteTooltip(GameObject tooltip) {
		tooltip.transform.position = this.HUDPopupPosition.position;
		tooltip.SetActive(true);
	}

	//private methods
	IEnumerator			DrawTooltipCoroutine(GameObject tooltip, float duration) {
		yield return new WaitForSeconds(duration);
		tooltip.SetActive(false);
	}


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
	private	void		Awake() {
		HUDManager._instance = this;
	}

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
