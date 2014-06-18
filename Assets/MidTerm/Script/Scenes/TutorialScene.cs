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

	//private attributes
	private	int		dieCount = 0;

	public	GameObject			KeysHint;
	public	List<ToolTipDatas>	OnRespawnLabels = new List<ToolTipDatas>();
	public	List<ToolTipDatas>	ToolTips = new List<ToolTipDatas>();

	#region IGameScene implementation

	//public methods
	public	void			ProcessEvent(string eventName) {
		switch (eventName) {
		case  "DisplayKeysHint":
			this.KeysHint.SetActive(true);
			break;
		case  "HideKeysHint":
			this.KeysHint.SetActive(false);
			break;
		default:
			break;
		}
//		this.DisplayToolTip(eventName);
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

	public	void			DisplayDeathToolTip(int idx) {
		ToolTipDatas tdata = this.OnRespawnLabels[idx];
		float duration = tdata.duration;
		if (tdata.infinite) duration = -1.0f;
		HUDManager.Instance.DrawTooltip(tdata.texts, duration);
	}

	//private methods
	private IEnumerator		WaitAnyKeyDown() {
		while (!Input.anyKeyDown) yield return null;
	}

	private	IEnumerator		StartScene() {
		this.DisplayToolTip("welcome");
		yield return new WaitForSeconds(1.0f);
		yield return this.StartCoroutine(this.WaitAnyKeyDown());
		HUDManager.Instance.HideCurrentToolTip();
		GameLevel.Instance.StartGame();
	}

	//private Unity callbacks
	private	void		Start() {
		GameLevel.Instance.GameScene = this;
//		GameLevel.Instance.StartGame();
//		this.StartCoroutine(this.StartScene());
//		Runity.Messenger<string>.AddListener("Player.Dead", this.onPlayerDied);
	}

	//private Runity callbacks
	private	void		onPlayerDied(string goname) {
		this.dieCount++;
		if (this.dieCount == 1) {
			this.DisplayDeathToolTip(0);
		} else if (this.dieCount >= 2 && this.dieCount <= 4) {
			this.DisplayDeathToolTip(Random.Range(1,4));
		} else {
			this.DisplayDeathToolTip(Random.Range(4,10));
		}
	}
}
