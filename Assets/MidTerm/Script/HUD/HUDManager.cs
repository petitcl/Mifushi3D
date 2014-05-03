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

	public	GameObject		HUDToolTip;
	public	Transform		HUDToolTipPosition;
	public	GameObject		HUDToolTipLinePrefab;


	//private attributes
	public	GameObject		CurrentToolTip;


	//public methods
	public	void		DrawTooltip(GameObject tooltip) {
		this.DrawTooltip(tooltip, 2.0f);
	}

	public	void		DrawTooltip(GameObject tooltip, float duration) {
		this.DrawInfiniteTooltip(tooltip);
		this.StartCoroutine(this.DrawTooltipCoroutine(tooltip, duration));
	}

	public	void		DrawTooltip(string[] texts, float duration) {
		if (this.CurrentToolTip != null) this.CurrentToolTip.SetActive(false);
		this.HUDToolTip.SetActive(true);
		this.CurrentToolTip = this.HUDToolTip;
		foreach (Transform t in this.HUDToolTipPosition.transform) {
			GameObject.Destroy(t.gameObject);
		}

		int i = 0;
		foreach (string text in texts) {
			GameObject newLine = GameObject.Instantiate(this.HUDToolTipLinePrefab,
			                                            this.HUDToolTipPosition.transform.position  + new Vector3(0.0f, -0.4f, 0.0f) * i,
			                                            Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
			TextMesh tm = newLine.GetComponent<TextMesh>();
			newLine.transform.parent = this.HUDToolTipPosition.transform;
			tm.text = text;
			++i;
		}
		if (duration > 0.0f)
			this.StartCoroutine(this.DrawTooltipCoroutine(this.CurrentToolTip, duration));
	}

	public	void		DrawInfiniteTooltip(GameObject tooltip) {
		tooltip.transform.position = this.HUDPopupPosition.position;
		tooltip.SetActive(true);
		if (this.CurrentToolTip != null) this.CurrentToolTip.SetActive(false);
		this.CurrentToolTip = tooltip;
	}

	public void			HideCurrentToolTip() {
		if (this.CurrentToolTip == null) return;
		this.CurrentToolTip.SetActive(false);
		this.CurrentToolTip = null;
	}

	//private methods
	//some refactor to do on param tooltip
	IEnumerator			DrawTooltipCoroutine(GameObject tooltip, float duration) {
		yield return new WaitForSeconds(duration);
		tooltip.SetActive(false);
		this.CurrentToolTip = null;
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
		//TODO : make this more pretty
//		GUI.Label(new Rect(20, 20, 100, 20), "Time : ");
//		GUI.Label(new Rect(130, 20, 100, 20), GameLevel.Instance.TimeSinceStart.ToString());
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
