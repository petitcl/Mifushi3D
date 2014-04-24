using UnityEngine;
using System.Collections;

namespace Runity {
	
	public class MonoBehaviourExt : MonoBehaviour {
		
		//public types
		public	delegate	void	SimpleCallback();

		//public attributes

		public	string		timeLayerName = "default";

		//public properties
		public	float		customDeltaTime {
			get {
				return Runity.TimeManager.Instance.GetDeltaTime(this.timeLayerName);
			}
		}

		//public methods
		public	void		WaitForAnimation(Animation anim, SimpleCallback cb) {
			this.StartCoroutine(this._WaitForAnimation(anim, cb));
		}
		
		public void			WaitForNSeconds(float time, SimpleCallback cb) {
			this.StartCoroutine(this._WaitForNSeconds(time, cb));
		}
		
		public void			WaitForSound(AudioSource source, SimpleCallback cb) {
			this.StartCoroutine(this._WaitForSound(source, cb));
		}
		
		//private methods
		private	IEnumerator	_WaitForAnimation(Animation anim, SimpleCallback cb) {
			do {
		        yield return null;
		    } while (anim.isPlaying);
			cb();
		}
		
		private IEnumerator	_WaitForNSeconds(float time, SimpleCallback cb) {
			yield return new WaitForSeconds(time);
			cb();
		}
		
		private	IEnumerator	_WaitForSound(AudioSource source, SimpleCallback cb) {
			do {
		        yield return null;
		    } while (source.isPlaying);
			cb();
	
		}
	}
	
}
