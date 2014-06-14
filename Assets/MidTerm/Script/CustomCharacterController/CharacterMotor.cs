using UnityEngine;
using System.Collections;

public class CharacterMotor : MonoBehaviour {

	public CharacterController Ctrl;

	public float JumpForce = 50;
	public float GravityForce = -8;
	private float JumpFallSpeed = 0;
	private bool HasToJump = false;

	public float StrafCoefSpeed = 1;
	public float ForwardCoefSpeed = 1;
	public float BackwardCoefSpeed = 1;
	private Vector3 MoveVector = new Vector3();

	public float SlidingCoefSpeed = 1;
	public LayerMask	SlidingLayerMask;
	public bool IsSliding {
		get; private set;
	}
	private RaycastHit SlideHit;

	public Vector3 LastResultVector {
		get; private set;
	}

	// Use this for initialization
	private void Start () {
		Ctrl = this.GetComponent<CharacterController>();
		IsSliding = false;
	}

	private void FixedUpdate() {
		Vector3 resultVector = Vector3.zero;

		this.ApplySlide(ref resultVector);
		this.ApplyJumpFall(ref resultVector);
		this.ApplyMove(ref resultVector);
		this.LastResultVector = resultVector;
		Debug.DrawRay(transform.position, resultVector, Color.green);
		this.Ctrl.Move(resultVector * Time.deltaTime);
	}

	public void ApplyJumpFall(ref Vector3 resultVector) {
		//Si on touche le sol ou le plafond on reset Jump/FallSpeed
		if ((this.Ctrl.collisionFlags & (CollisionFlags.Above | CollisionFlags.Below)) != 0) {
			this.JumpFallSpeed = 0;
		}

		//Apply Jump
		if (this.HasToJump && this.Ctrl.isGrounded && !this.IsSliding) {
			this.JumpFallSpeed += this.JumpForce;
		}
		this.HasToJump = false;

		//Apply Gravity
		this.JumpFallSpeed += this.GravityForce;
		resultVector.y += this.JumpFallSpeed;
	}

	public void ApplyMove(ref Vector3 resultVector) {
		if (!this.IsSliding) {
			if (this.MoveVector.x > 0) {
				resultVector += this.MoveVector.x * this.ForwardCoefSpeed * this.transform.forward;
			} else {
				resultVector += this.MoveVector.x * this.BackwardCoefSpeed * this.transform.forward;
			}
		}
		resultVector += this.MoveVector.z * this.StrafCoefSpeed * this.transform.right;
	}

	private void ApplySlide(ref Vector3 resultVector) {
		if (!Ctrl.isGrounded) return;
		if (!Physics.Raycast(this.transform.position + Vector3.up,
		                    Vector3.down,
		                    out this.SlideHit, Mathf.Infinity, this.SlidingLayerMask)) {
		}
		//TODO mettre en parametre les coefs de pente
		if (this.SlideHit.normal.y < 0.99f) {
			Vector3 slideVector = CreateSlideVector(this.SlideHit.normal);
			slideVector *= this.SlidingCoefSpeed;
			Debug.DrawRay(this.transform.position, slideVector, Color.red);
			if (this.SlideHit.normal.y < 0.7f) {
				resultVector = slideVector;
			} else {
				resultVector += slideVector;
			}
			this.IsSliding = true;
		} else {
			this.IsSliding = false;
		}
	}

	private static Vector3 CreateSlideVector(Vector3 n) {
		return Vector3.down - Vector3.Dot(n, Vector3.down) * n;
		//return Vector3.Cross(n, Vector3.Cross(Vector3.down, n));
	}

	public void Move(float forward, float straf) {
		this.MoveVector.x = forward;
		this.MoveVector.z = straf;
		if (this.MoveVector.magnitude > 1) {
			this.MoveVector.Normalize();
		}
	}
	
	public void Jump() {
		this.HasToJump = true;
	}
}
