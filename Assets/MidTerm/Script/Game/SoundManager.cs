using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public static SoundManager Instance = null;
	public enum GameEvent
	{
		PlayerJump,
		PlayerDie,
		PickObject,
		DropObject
	};
	public AudioClip PlayerJumpSound;
	public AudioClip PlayerDieSound;
	public AudioClip PickObjectSound;
	public AudioClip DropObjectSound;
	void Awake(){
		if (Instance != null) {
			throw new UnityException();
		}
		DontDestroyOnLoad (this);
		Instance = this;
	}
	public void Play(SoundManager.GameEvent e)
	{
		switch (e) {
		case GameEvent.PlayerJump:
			if (this.PlayerJumpSound == null) {
				return ;
			}
			//this.audio.clip = this.PlayerJumpSound;
			this.audio.PlayOneShot (this.PlayerJumpSound);
			break;
		case GameEvent.PlayerDie:
			if (this.PlayerDieSound == null) {
				return ;
			}
			//this.audio.clip = this.PlayerDieSound;
			this.audio.PlayOneShot (this.PlayerDieSound);
			break;
		case GameEvent.PickObject:
			if (this.PickObjectSound == null) {
				return ;
			}
			//this.audio.clip = this.PickObjectSound;
			this.audio.PlayOneShot (this.PickObjectSound);
			break;
		case GameEvent.DropObject:
			if (this.DropObjectSound == null) {
				return ;
			}
			this.audio.clip = this.DropObjectSound;
			this.audio.PlayOneShot (this.DropObjectSound);
			break;

		}
	}
}
