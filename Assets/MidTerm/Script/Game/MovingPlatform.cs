using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public	Transform	startPosition;
	public	Transform	endPosition;
	public	bool		flip;
	public	float		speed;

	private void	Update() {
		float step = this.speed * Time.deltaTime;

		Transform destination = this.flip ? this.endPosition : this.startPosition;

		this.transform.position = Vector3.MoveTowards(this.transform.position, destination.position, step);
//		Debug.Log(this.transform.position + " == " + destination);
//		Debug.Log(Vector3.SqrMagnitude(this.transform.position - destination.position));
//		if (this.transform.position == destination.position || this.transform.position.Equals(destination.position)) {
		//wtf
		if (Vector3.SqrMagnitude(this.transform.position - destination.position) < 0.01f) {
			this.flip = !this.flip;
		}
	}
}
