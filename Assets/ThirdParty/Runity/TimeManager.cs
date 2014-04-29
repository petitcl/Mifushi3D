using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Runity {

	public class TimeManager {

		//static variables
		private	static	TimeManager	_instance = null;

		public	static	TimeManager	Instance {
			get {
				if (TimeManager._instance == null) {
					TimeManager._instance = new TimeManager();
				}
				return TimeManager._instance;
			}
		}


		//private attributes
		private	Dictionary<string, float>	timeLayers;

		//public methods
		public	void	SetTimeLayer(string name) {
			this.SetTimeLayer(name, 1.0f);
		}

		public	void	SetTimeLayer(string name, float	scale) {
			try {
				if (this.timeLayers.ContainsKey(name)) {
					this.timeLayers[name] = scale;
				} else {
					this.timeLayers.Add(name, scale);
				}
			} catch (System.Exception) {
				Debug.LogWarning("Runity.TimeManager: cannot find Time Layer \""+name+"\" !");
				return;
			}
		}

		public	float	GetDeltaTime() {
			return this.GetDeltaTime("default");
		}

		public	float	GetDeltaTime(string name) {
			try {
				float timeScale = this.timeLayers[name];
				return Time.deltaTime * timeScale;
			} catch (System.Exception) {
				Debug.LogWarning("Runity.TimeManager: cannot find Time Layer \""+name+"\" !");
				return 0.0f;
			}
		}

		//@TODO: add GetTime(string name) & GetTimeScale


		//private methods

		private	TimeManager() {
			this.timeLayers = new Dictionary<string, float>();
			this.SetTimeLayer("default");
		}

	}

}