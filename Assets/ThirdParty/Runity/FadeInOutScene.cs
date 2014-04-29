using UnityEngine;
using System.Collections;

namespace Runity {
	
	public class FadeInOutScene : MonoBehaviour {
	
		//public attributes
		public	Texture2D	image;
		public	float	fadeInTime = 1.0f;
		public	float	fadeOutTime = 1.0f;
		public	float	stayTime = 1.5f;
		public	string	nextScene = "mainMenu";
		
		//public links
		
		//private attributes
		private	float	_currentTime = 0.0f;
		private	int		_state = 0;
		private	float	_alpha = 0.0f;
		private	bool	_done = false;
		
		//private methods
		private void	Start () {
		
		}
		
		private	void	Update () {
			if (this._done) {
				return;
			}
			this._currentTime += Time.deltaTime;
			if (this._state == 0) {
				this._alpha = (this._currentTime / this.fadeInTime);
				if (this._currentTime > this.fadeInTime) {
					this._state = 1;
					this._alpha = 1.0f;
					this._currentTime = 0.0f;
				}
			} else if (this._state == 1) {
				this._alpha = 1.0f;
				if (this._currentTime > this.stayTime) {
					this._state = 2;
					this._currentTime = 0.0f;
				}
			} else if (this._state == 2) {
				this._alpha = 1.0f - (this._currentTime / this.fadeOutTime);
				if (this._currentTime > this.fadeOutTime) {
					this._state = 3;
					this._alpha = 0.0f;
					this._done = true;
					Application.LoadLevel(this.nextScene);
				}
			}
		}
		
		private	void	OnGUI() {
			//Debug.Log("alpha=" + this._alpha + " _state=" + this._state + " _currentTime" + this._currentTime);
			GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, this._alpha);
			GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), this.image);
		}
	}
	
}

