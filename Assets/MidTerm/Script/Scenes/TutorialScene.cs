using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO : make this class and all other class inherit from TutorialScene
public class TutorialScene : MonoBehaviour, IGameScene {

	//public types
	[System.Serializable]
	public	class ToolTipDatas {
		public	string			name;
		public	string[]		texts;
		public	float			duration;
		public	bool			infinite = false;
	}

	//public attributes
	public	GameObject		WelcomeToolTip;
	public	GameObject		CheckPointToolTip;
	public	GameObject		ChangeColorToolTip;

	public	List<ToolTipDatas>	OnRespawnLabels = new List<ToolTipDatas>();
	public	List<ToolTipDatas>	ToolTips = new List<ToolTipDatas>();
	#region IGameScene implementation

	//public methods

	public	void			ProcessEvent(string eventName) {
		switch (eventName) {
		case "FirstCheckPoint":
			this.FirstCheckPointAnimation();
			break;
		case "ChangeColor":
			this.ChangeColorAnimation();
			break;
		default:
			break;
		}
	}

	#endregion

	//public methods
	public	void			DisplayToolTip(string tname) {
		foreach (ToolTipDatas tdata in this.ToolTips) {
			if (!tdata.name.Equals(tname)) continue;
			float duration = tdata.duration;
			if (tdata.infinite) duration = -1.0f;
			HUDManager.Instance.DrawTooltip(tdata.texts, duration);
			return;
		}
	}


	//private methods
	private IEnumerator		WaitAnyKeyDown() {
		while (!Input.anyKeyDown) yield return null;
	}

	private	IEnumerator		StartScene() {
//		HUDManager.Instance.DrawInfiniteTooltip(this.WelcomeToolTip);
		this.DisplayToolTip("welcome");
		yield return new WaitForSeconds(1.0f);
		yield return this.StartCoroutine(this.WaitAnyKeyDown());
		HUDManager.Instance.HideCurrentToolTip();
		GameLevel.Instance.StartGame();
		string[] labels = new string[2];
		labels[0] = "this is a test lol";
		labels[1] = "this is an elephant";
		HUDManager.Instance.DrawTooltip(labels, 15.0f);
	}

	private	void			FirstCheckPointAnimation() {
		HUDManager.Instance.DrawTooltip(this.CheckPointToolTip, 5.0f);
	}
	private	void			ChangeColorAnimation() {
		HUDManager.Instance.DrawTooltip(this.ChangeColorToolTip, 10.0f);
	}
	//private Unity callbacks

	private	void		Start() {
		GameLevel.Instance.GameScene = this;
		this.StartCoroutine(this.StartScene());
	}
}
