using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Animation_Manager : MonoBehaviour {

	public bool Left = false;
	public bool Right = false;
	public bool Forward = false;
	public bool Backward = false;

	public MotionStateList CharacterMotionState = MotionStateList.Stationary;

	public static Animation_Manager Instance;
	
	[System.Serializable]
	public enum MotionStateList {
		Stationary,
		Forward,
		Backward,
		Left,
		Right,
		LeftForward,
		RightForward,
		LeftBackward,
		RightBackward,
		Jump
	}

	[System.Serializable]
	public class AnimationStateDefinition
	{
		public MotionStateList state;
		public AnimationClip clip;
	};

	public List<AnimationStateDefinition> animations = new List<AnimationStateDefinition>();
	
	private void Awake() {
		Instance = this;
	}

	private void update() {
		this.CurrentMotionState ();
	}

	public void CurrentAnimation() {
		foreach (AnimationStateDefinition st in this.animations) {
			if (this.CharacterMotionState == st.state) {
				this.animation.playAutomatically = true;
				if (!this.animation.IsPlaying(st.clip.name)) {
					this.animation.Play(st.clip.name);
				}
			}
		}
	}

	public void CurrentMotionState() {

		this.Left = false;
		this.Right = false;
		this.Forward = false;
		this.Backward = false;

		Vector3 moveVector = Character_Motor.Instance.MoveVector;
		if (moveVector.y < 0) {
			this.Forward = true;
		} else if (moveVector.y > 0) {
			this.Backward = true;
		}
	
		if (moveVector.x > 0) {
			this.Right = true;
		} else if (moveVector.x < 0) {
			this.Left = true;
		}

		if (Character_Manager.Instance.isJumping() || !Character_Manager.CharacterControllerComponent.isGrounded) {
		    Animation_Manager.Instance.CharacterMotionState = Animation_Manager.MotionStateList.Jump;
			CurrentAnimation();
			return;
		}
		  

		if (this.Left) {
			if (this.Forward) {
				this.CharacterMotionState = MotionStateList.LeftForward;
			} else if(this.Backward) {
				this.CharacterMotionState = MotionStateList.LeftBackward;
			} else {
				this.CharacterMotionState = MotionStateList.Left;
			}
		} else if (this.Right) {
			if (this.Forward) {
				this.CharacterMotionState = MotionStateList.RightForward;
			} else if(this.Backward) {
				this.CharacterMotionState = MotionStateList.RightBackward;
			} else {
				this.CharacterMotionState = MotionStateList.Right;
			}
		} else if (this.Forward) {
			this.CharacterMotionState = MotionStateList.Forward;
		} else if (this.Backward) {
			this.CharacterMotionState = MotionStateList.Backward;
		}

		if (!this.Left && !this.Right && !this.Forward && !this.Backward) {
			this.CharacterMotionState = MotionStateList.Stationary;
		}
		this.CurrentAnimation();
	}
}
