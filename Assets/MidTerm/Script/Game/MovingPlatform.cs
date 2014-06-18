using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public	Transform	startPosition;
	public	Transform	endPosition;
	public	bool		flip;
	public	float		speed;
	public	bool		isTime = false;
	public	float		delayFlip = 0;
	public	float		delayUnFlip = 0;
	public	bool		enabledAtStart = true;

	private	float		timeLastFlip;
	private	float		timeLastFlipEnded;

	private void Start() {
		timeLastFlip = Time.time;
		timeLastFlipEnded = Time.time;
		if (!this.enabledAtStart) this.enabled = false;
	}

	private void	FixedUpdate() {
		if (!this.isTime) {
			float step = this.speed * Time.deltaTime;
			
			Transform destination = this.flip ? this.endPosition : this.startPosition;
			
			//Stop moving during flip
			if (Time.time > timeLastFlipEnded) {
				this.transform.position = Vector3.MoveTowards(this.transform.position, destination.position, step);
				if (Vector3.SqrMagnitude(this.transform.position - destination.position) < 0.01f) {
					timeLastFlip = Time.time;
					this.flip = !this.flip;
					timeLastFlipEnded = Time.time + GetDelay();
				}
			}
		}
		else {
//			float arrivalTime = this.timeLastFlipEnded + this.speed;
			float currentOffset = (Time.time - this.timeLastFlipEnded) / (this.speed);
			currentOffset = Mathf.Clamp01(currentOffset);
//			Debug.Log(this.timeLastFlipEnded);
//			Debug.Log(arrivalTime);
//			Debug.Log(currentOffset);

			Transform start = !this.flip ? this.endPosition : this.startPosition;
			Transform destination = this.flip ? this.endPosition : this.startPosition;

			if (Time.time > timeLastFlipEnded) {
				this.transform.position = Vector3.Lerp(start.position, destination.position, currentOffset);
				if (Vector3.SqrMagnitude(this.transform.position - destination.position) < 0.01f) {
					timeLastFlip = Time.time;
					this.flip = !this.flip;
					timeLastFlipEnded = Time.time + GetDelay();
				}
			}
		}
	}

	private float GetDelay() {
		if (flip) return delayFlip;
		return delayUnFlip;
	}
}
