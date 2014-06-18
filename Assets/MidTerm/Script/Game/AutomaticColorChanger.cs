using UnityEngine;
using System.Collections;


[RequireComponent (typeof (ColorBlock))]
public class AutomaticColorChanger : MonoBehaviour {

	public	GameLevel.GameColor[]	Colors;
	public	float			TransitionDelay = 1.0f;
	public	float			StartDelay = 0;

	private	float			LastTransitionTime = 0.0f;
	private	int				CurrentColorIndex = 0;

	private void	Start() {
		LastTransitionTime += StartDelay;
	}

	private void	Update() {

		if (Time.time > this.LastTransitionTime + this.TransitionDelay) {
			CurrentColorIndex++;
			if (CurrentColorIndex >= this.Colors.Length) CurrentColorIndex = 0; 

			GetComponent<ColorBlock>().SetColor(this.Colors[this.CurrentColorIndex]);
			this.LastTransitionTime = Time.time;
		}

	}

}
