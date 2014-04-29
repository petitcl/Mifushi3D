using UnityEngine;
using System.Collections;

public class SceneObject : MonoBehaviour {

	//public attributes
	public GameLevel.GameColor	StartColor = GameLevel.GameColor.Default;

	//public properties
	public GameLevel.GameColor	CurrentColor { get; private set; } 

	//private attributes
	private	Material	savedMaterial = null;

	//public methods
	public	void	SetColor(GameLevel.GameColor newColor) {
		if (this.CurrentColor.Equals(newColor)) return;
		this.savedMaterial = this.renderer.sharedMaterial;
		this.gameObject.SetLayerRecursively(GameLevel.Instance.GameColorToLayerMask(newColor));
		this.CurrentColor = newColor;
		GameAnimator.Instance.MatAnimator.TempFade(this.renderer.material, GameLevel.Instance.GameColorToColor(newColor),
		                                           0.3f, this._onMaterialAnimationDone);
//		Debug.Log("tu vas changer ta couleur batard");
	}

	//private Unity callbacks
	private	void	Start() {
		this.CurrentColor = this.StartColor;
		Runity.Messenger<GameLevel.GameColor>.AddListener("Player.ChangeColor", this._onPlayerChangeColor);
	}

//	//private callbacks
	private	void	_onPlayerChangeColor(GameLevel.GameColor c) {
		this.BroadcastMessage("onPlayerChangeColor", c, SendMessageOptions.DontRequireReceiver);
	}

	private	void	_onMaterialAnimationDone() {
		this.renderer.material = GameAnimator.Instance.GameColorToMaterial(this.CurrentColor);
	}
}
