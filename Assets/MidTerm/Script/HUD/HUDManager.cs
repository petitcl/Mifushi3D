using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

	//public types
	public	class 	ToolTip {
		public	GameObject	gameObject;
		public	float		duration;

		public	ToolTip(GameObject pgameObject) {
			this.gameObject = pgameObject;
			this.duration = 2.0f;
		}

		public	ToolTip(GameObject pgameObject, float pduration) {
			this.gameObject = pgameObject;
			this.duration = pduration;
		}
	}

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
	private	ToolTip			CurrentToolTip;


	//public methods
	public	void		DrawTooltip(GameObject tooltip) {
		this.DrawTooltip(tooltip, 2.0f);
	}

	public	void		DrawTooltip(GameObject tooltip, float duration) {
		ToolTip	ttObj = new ToolTip(tooltip, duration);
		this.DrawInfiniteTooltip(ttObj);
		this.StartCoroutine(this.DrawTooltipCoroutine(ttObj));
	}

	public	void		DrawTooltip(string[] texts, float duration) {
		if (this.CurrentToolTip != null) this.CurrentToolTip.gameObject.SetActive(false);
		this.HUDToolTip.SetActive(true);
		ToolTip	ttObj = new ToolTip(this.HUDToolTip, duration);
		this.CurrentToolTip = ttObj;
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
			this.StartCoroutine(this.DrawTooltipCoroutine(ttObj));
	}

	public	void		DrawInfiniteTooltip(ToolTip	ttObj) {
		ttObj.gameObject.transform.position = this.HUDPopupPosition.position;
		ttObj.gameObject.SetActive(true);
		if (this.CurrentToolTip != null) this.CurrentToolTip.gameObject.SetActive(false);
		this.CurrentToolTip = ttObj;
	}

	public void			HideCurrentToolTip() {
		if (this.CurrentToolTip == null) return;
		this.CurrentToolTip.gameObject.SetActive(false);
		this.CurrentToolTip = null;
	}

	//private methods
	//some refactor to do on param tooltip
	IEnumerator			DrawTooltipCoroutine(ToolTip ttObj) {
		yield return new WaitForSeconds(ttObj.duration);
		if (this.CurrentToolTip != null && this.CurrentToolTip.Equals(ttObj)) {
			ttObj.gameObject.SetActive(false);
			this.CurrentToolTip = null;
		}
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
