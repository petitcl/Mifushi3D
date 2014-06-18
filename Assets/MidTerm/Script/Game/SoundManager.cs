using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public enum GameEvent
	{
		PlayerJump,
		PlayerDie,
		PickObject,
		DropObject,
		NewCheckpoint,
		SelectMenu
	};

	public AudioClip PlayerJumpSound;
	public AudioClip PlayerDieSound;
	public AudioClip PickObjectSound;
	public AudioClip DropObjectSound;
	public AudioClip CheckPointSound;
	public AudioClip SelectMenu;

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
			case GameEvent.SelectMenu: 
				if (this.SelectMenu == null) {
					return;
				}
				this.audio.PlayOneShot (this.SelectMenu);
				break;

			}
	}
}
