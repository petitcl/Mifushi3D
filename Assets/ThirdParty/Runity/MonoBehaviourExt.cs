using UnityEngine;
using System.Collections;

namespace Runity {

	//TODO : maybe refactor this class into a static extension class
	public class MonoBehaviourExt : MonoBehaviour {
		
		//public types
		public	delegate	void	EndTransitionCallback();

//		//public attributes
//		public	string		timeLayerName = "default";
//
//		//public properties
//		public	float		customDeltaTime {
//			get {
//				return Runity.TimeManager.Instance.GetDeltaTime(this.timeLayerName);
//			}
//		}

		//public methods
//		public	void		SetLayerRecursively(int layer) {
//			this.gameObject.layer = layer;
//			foreach (Transform child in this.gameObject.transform) {
//				child.gameObject
//			}
//		}

		public	void		WaitForAnimation(Animation anim, EndTransitionCallback cb) {
			this.StartCoroutine(this._WaitForAnimation(anim, cb));
		}
		
		public void			WaitForNSeconds(float time, EndTransitionCallback cb) {
			this.StartCoroutine(this._WaitForNSeconds(time, cb));
		}
		
		public void			WaitForSound(AudioSource source, EndTransitionCallback cb) {
			this.StartCoroutine(this._WaitForSound(source, cb));
		}
		
		//private methods
		private	IEnumerator	_WaitForAnimation(Animation anim, EndTransitionCallback cb) {
			do {
		        yield return null;
		    } while (anim.isPlaying);
			cb();
		}
		
		private IEnumerator	_WaitForNSeconds(float time, EndTransitionCallback cb) {
			yield return new WaitForSeconds(time);
			cb();
		}
		
		private	IEnumerator	_WaitForSound(AudioSource source, EndTransitionCallback cb) {
			do {
		        yield return null;
		    } while (source.isPlaying);
			cb();
	
		}
	}
	
}
