using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public	Transform	startPosition;
	public	Transform	endPosition;
	public	bool		flip;
	public	float		speed;
	public	float		delayFlip = 0;
	public	float		delayUnFlip = 0;
	public	bool		enabledAtStart = true;

	private	float		timeLastFlip;

	private void Start() {
		timeLastFlip = 0;
		if (!this.enabledAtStart) this.enabled = false;
	}

	private void	Update() {
		float step = this.speed * Time.deltaTime;

		Transform destination = this.flip ? this.endPosition : this.startPosition;

		//Stop moving during flip
		if (Time.time - timeLastFlip > GetDelay()) {
			this.transform.position = Vector3.MoveTowards(this.transform.position, destination.position, step);
			if (Vector3.SqrMagnitude(this.transform.position - destination.position) < 0.01f) {
				this.flip = !this.flip;
				timeLastFlip = Time.time;
			}
		}
	}

	private float GetDelay() {
		if (flip) return delayFlip;
		return delayUnFlip;
	}
}
