using UnityEngine;
using System.Collections;

namespace Runity {
	public class FadeInOut : MonoBehaviour {
		//public attributes
		public	float		fadeTime = 2.0f;
		public	Texture2D	texture;

		//private attributes
		private	bool	fadeDir = true;
		private	float	alpha = 1.0f;
		private	float	startTime = 0.0f;

		//public methods
		public	void	FadeIn() {
			this.fadeDir = true;
			this.startTime = Time.time;
			this.alpha = 1.0f;
		}

		public	void	FadeOut() {
			this.fadeDir = false;
			this.startTime = Time.time;
			this.alpha = 0.0f;
		}

		//private Unity callbacks
//		private	void	Start() {
//			this.FadeIn();
//		}

		private	void	Update() {
			float currTime = Time.time - this.startTime;
			if (currTime == 0.0f) {
				return;
			}
			float offset = currTime / this.fadeTime;
			if (this.fadeDir) {
				this.alpha = 1.0f - offset;
			} else {
				this.alpha = offset;
			}
			this.alpha = Mathf.Clamp01(this.alpha);
		}

		private	void	OnGUI() {
			GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, this.alpha);
			GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), this.texture);
		}
	}
}
