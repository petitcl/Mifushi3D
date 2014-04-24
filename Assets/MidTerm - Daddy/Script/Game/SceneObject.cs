using UnityEngine;
using System.Collections;

public class SceneObject : MonoBehaviour {

	//public attributes
	public GameLevel.GameColor	gameColor = GameLevel.GameColor.Default;

	//private Unity callbacks
	private	void	Start() {
		Runity.Messenger<GameLevel.GameColor>.AddListener("Player.ChangeColor", this._onPlayerChangeColor);
		this.onStart();
	}


	//private callbacks
	private	void	_onPlayerChangeColor(GameLevel.GameColor c) {
		this.onPlayerChangeColor(c);
	}

	//protected virtual callbacks
	protected virtual	void	onStart() {

	}

	protected virtual	void	onPlayerChangeColor(GameLevel.GameColor c) {
		if (this.gameColor == GameLevel.GameColor.Default) {
			return;
		}
		Color nColor = this.GetComponent<SpriteRenderer>().color;
		if (this.gameColor == c) {
			nColor.a = 1.0f;
		} else {
			nColor.a = 0.5f;
		}
		this.GetComponent<SpriteRenderer>().color = nColor;
	}
}
