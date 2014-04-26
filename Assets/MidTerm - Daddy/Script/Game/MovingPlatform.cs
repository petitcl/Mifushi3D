using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public	Transform	startPosition;
	public	Transform	endPosition;
	public	bool		flip;
	public	float		speed;

	private void	Update() {
		float step = this.speed * Time.deltaTime;

		Vector3 destination = this.flip ? this.endPosition.position : this.startPosition.position;

		this.transform.position = Vector3.MoveTowards(this.transform.position, destination, step);
		if (this.transform.position == destination) {
			this.flip = !this.flip;
		}
	}
}
