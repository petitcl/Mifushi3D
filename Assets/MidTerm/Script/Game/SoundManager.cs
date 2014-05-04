using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public static SoundManager Instance = null;
	public enum GameEvent
	{
		PlayerJump,
		PlayerDie,
		PickObject,
		DropObject,
		NewCheckpoint
	};
	public AudioClip PlayerJumpSound;
	public AudioClip PlayerDieSound;
	public AudioClip PickObjectSound;
	public AudioClip DropObjectSound;
	public AudioClip CheckPointSound;
	void Awake(){
		if (Instance != null) {
			return ;
		}
		DontDestroyOnLoad (this);
		Instance = this;
	}
	public void Play(SoundManager.GameEvent e)
	{
		switch (e) {
			case GameEvent.PlayerJump:
				if (this.PlayerJumpSound == null) {
						return;
				}
				this.audio.PlayOneShot (this.PlayerJumpSound);
				break;
			case GameEvent.PlayerDie:
				if (this.PlayerDieSound == null) {
						return;
				}
				this.audio.PlayOneShot (this.PlayerDieSound);
				break;
			case GameEvent.PickObject:
				if (this.PickObjectSound == null) {
						return;
				}
				this.audio.PlayOneShot (this.PickObjectSound);
				break;
			case GameEvent.DropObject:
				if (this.DropObjectSound == null) {
						return;
				}
				this.audio.PlayOneShot (this.DropObjectSound);
				break;
			case GameEvent.NewCheckpoint: 
				if (this.CheckPointSound == null) {
						return;
				}
				this.audio.PlayOneShot (this.CheckPointSound);
				break;
			}
	}
}
