using UnityEngine;
using System.Collections;

//TODO : make this class and all other class inherit from TutorialScene
public class TutorialScene : MonoBehaviour {

	//public attributes
	public	GameObject		WelcomeToolTip;
	
	//private methods
	private IEnumerator		WaitAnyKeyDown() {
		while (!Input.anyKeyDown) yield return null;
	}

	private	IEnumerator		StartScene() {
		HUDManager.Instance.DrawInfiniteTooltip(this.WelcomeToolTip);
		yield return new WaitForSeconds(1.0f);
		yield return this.StartCoroutine(this.WaitAnyKeyDown());
		this.WelcomeToolTip.SetActive(false);
		GameLevel.Instance.StartGame();
	}

	//private Unity callbacks
	private	void		Start() {
		this.StartCoroutine(this.StartScene());
	}
}
