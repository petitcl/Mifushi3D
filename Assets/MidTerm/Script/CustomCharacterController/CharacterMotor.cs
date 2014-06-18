using UnityEngine;
using System.Collections;

public class CharacterMotor : MonoBehaviour {

	public CharacterController Ctrl;
	private CharacterManager manager;

	public float JumpForce = 50;
	public float GravityForce = -8;
	private float JumpFallSpeed = 0;
	private bool HasToJump = false;

	private bool boost;
	private float CurrentBoostMultiplcator = 1;
	public float BoostMultiplicator = 2;
	public float BoostSmooth = 2;
	public float BoostBlockStrafThreshold;

	public float StrafCoefSpeed = 1;
	public float ForwardCoefSpeed = 1;
	public float BackwardCoefSpeed = 1;

	private Vector3 MoveVector = new Vector3();
	private Vector3 SlideVector = new Vector3();

	public float SlidingCoefSpeed = 1;
	public LayerMask	SlidingLayerMask;
	public bool IsSliding {
		get; private set;
	}
	private RaycastHit SlideHit;

	public Vector3 LastResultBoostr {
		get; private set;
	}

	// Use this for initialization
	private void Start () {
		Ctrl = this.GetComponent<CharacterController>();
		manager = this.GetComponent<CharacterManager>();
		IsSliding = false;
	}

	private void FixedUpdate() {
		Vector3 resultVector = Vector3.zero;
		this.ApplySlide(ref resultVector);
		this.ApplyJumpFall(ref resultVector);
		this.ApplyMove(ref resultVector);
		Debug.DrawRay(transform.position, resultVector, Color.green);
		this.manager.animator.SetFloat("VerticalSpeed", resultVector.y);
		this.manager.animator.SetBool("IsGrounded", this.Ctrl.isGrounded);
		this.manager.animator.SetFloat("HorizontalSpeed", this.MoveVector.magnitude);
		this.manager.animator.SetFloat("SpeedForward", this.MoveVector.x);
		this.manager.animator.SetFloat("SpeedStraf", this.MoveVector.z);
		this.manager.animator.SetBool("IsSliding", this.IsSliding);
		this.manager.animator.SetFloat("SlidingSpeed", this.SlideVector.magnitude);
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
			this.manager.animator.SetTrigger("Jump");
		}
		this.HasToJump = false;

		//Apply Gravity
		this.JumpFallSpeed += this.GravityForce;
		resultVector.y += this.JumpFallSpeed;
	}

	public void ApplyMove(ref Vector3 resultVector) {
		if (this.boost) {
			this.CurrentBoostMultiplcator = Mathf.Lerp(this.CurrentBoostMultiplcator, this.BoostMultiplicator, Time.deltaTime * this.BoostSmooth);
		} else {
			this.CurrentBoostMultiplcator = Mathf.Lerp(this.CurrentBoostMultiplcator, 1, Time.deltaTime * this.BoostSmooth);
		}
		if (!this.IsSliding) {
			if (this.MoveVector.x > 0) {
				resultVector += this.MoveVector.x * this.ForwardCoefSpeed * this.CurrentBoostMultiplcator * this.transform.forward;
			} else {
				resultVector += this.MoveVector.x * this.BackwardCoefSpeed * this.CurrentBoostMultiplcator  * this.transform.forward;
			}
		}
		if (this.CurrentBoostMultiplcator < this.BoostBlockStrafThreshold) {
			resultVector += this.MoveVector.z * this.StrafCoefSpeed * this.transform.right;
		}
	}

	private void ApplySlide(ref Vector3 resultVector) {
		if (!Ctrl.isGrounded) return;
		RaycastHit _hit;
		if (Physics.Raycast(this.transform.position + Vector3.up,
		                    Vector3.down,
		                    out _hit, 3.0f, this.SlidingLayerMask)) {
			this.SlideHit = _hit;
		}
//		CharacterController charContr = this.transform.GetComponent<CharacterController>();
//		Vector3 p1 = this.transform.position + Vector3.up * charContr.height * 0.5F;
//		Vector3 p2 = p1 + Vector3.up * -charContr.height;
//		if (Physics.CapsuleCast(p1, p2, charContr.radius, Vector3.down, out _hit, 3.0f, this.SlidingLayerMask)) {
//			this.SlideHit = _hit;
//		}

		//TODO mettre en parametre les coefs de pente
//		if (this.SlideHit.collider != null) {
//			Debug.Log(this.SlideHit.collider.name);
//			Debug.DrawRay(this.transform.position, this.transform.position + this.SlideHit.normal * 10.0f);
//		}

		if (this.SlideHit.collider != null && this.SlideHit.normal.y < 0.99f) {
			this.SlideVector = CreateSlideVector(this.SlideHit.normal);
			this.SlideVector *= this.SlidingCoefSpeed;
			Debug.DrawRay(this.transform.position, this.SlideVector, Color.red);
			if (this.SlideHit.normal.y < 0.7f) {
				resultVector = this.SlideVector;
			} else {
				resultVector += this.SlideVector;
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

	public void Boost(bool activate) {
		this.boost = activate;
	}
}
