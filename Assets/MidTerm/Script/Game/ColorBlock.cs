using UnityEngine;
using System.Collections;
using Runity.ExtensionMethods;

public class ColorBlock : MonoBehaviour {

	//public attributes
	public GameLevel.GameColor	StartColor = GameLevel.GameColor.Default;

	//public properties
	public GameLevel.GameColor	CurrentColor { get; private set; }

	//private attributes
	private	Material	savedMaterial = null;

	//public methods
	public	void	SetColor(GameLevel.GameColor newColor) {
		if (this.CurrentColor.Equals(newColor)) return;
		if (this.renderer != null) {
			this.savedMaterial = this.renderer.sharedMaterial;
			GameAnimator.Instance.MatAnimator.TempFade(this.renderer.material, GameLevel.Instance.GameColorToFadedColor(newColor),
			                                           0.3f, this._onMaterialAnimationDone);
		}
		this.gameObject.SetLayerRecursively(GameLevel.Instance.GameColorToLayerMask(newColor));
		this.CurrentColor = newColor;
//		if (GameLevel.Instance.Player.GetComponent<ColorCharacterController>().CurrentColor == this.CurrentColor) {
//		} else {
//			GameAnimator.Instance.MatAnimator.TempFade(this.renderer.material, GameLevel.Instance.GameColorToColor(newColor),
//			                                           0.3f, this._onMaterialAnimationDone);
//		}
//		Debug.Log("tu vas changer ta couleur batard");
	}

	//private Unity callbacks
	private	void	Start() {
		this.CurrentColor = this.StartColor;
	}

//	private	void	Update() {
//		if (Input.GetKeyDown(KeyCode.P) && this.CurrentColor == GameLevel.GameColor.Blue) {
//			this.SetColor(GameLevel.GameColor.Red);
//		} else if (Input.GetKeyDown(KeyCode.P) && this.CurrentColor == GameLevel.GameColor.Red) {
//			this.SetColor(GameLevel.GameColor.Blue);
//		}
//	}

//	//private callbacks
	private	void	_onMaterialAnimationDone(Material mat) {
		this.renderer.material = GameAnimator.Instance.GameColorToMaterial(this.CurrentColor);
	}
}
