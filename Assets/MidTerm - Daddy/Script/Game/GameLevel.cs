﻿using UnityEngine;
using System.Collections;

public class GameLevel : MonoBehaviour {

	//public types
	public	enum GameColor {
		Red,
		Green,
		Blue,
		Default
	}

	//static attributes
	private	static	GameLevel	_instance = null;
	public	static	GameLevel	Instance {
		get {
//			if (GameLevel._instance == null) {
//				GameLevel._instance = new GameLevel();
//			}
			return GameLevel._instance;
		}
	}

	//public attributes
	public	Color	Red;
	public	Color	Green;
	public	Color	Blue;
	public	Color	White;
	
	public	Color	FadedRed;
	public	Color	FadedGreen;
	public	Color	FadedBlue;

	public	string	RedLayerName = "Physics_Red";
	public	string	GreenLayerName = "Physics_Green";
	public	string	BlueLayerName = "Physics_Blue";
	public	string	PlayerLayerName = "Player";

	public	GameObject	PlayerPrefab;

	public	Transform	PlayerSpawnPoint;

	public	GameObject	Player;

	//public properties
	public	int		RedLayerMask {get; private set;}
	public	int		GreenLayerMask {get; private set;}
	public	int		BlueLayerMask {get; private set;}
	public	int		PlayerLayerMask {get; private set;}

	public	bool	Started {get; private set;}
	public	bool	Paused {get; private set;}
	public	bool	Finished {get; private set;}
	public	bool	Running {
	get {
			return this.Started && !this.Paused && !this.Finished;
		}
	}

	//private attributes
	private CheckPoint	lastCheckPoint;

	//Pause
	public GameObject pauseScreen;

	//private Unity methods 
	private	void	Awake() {

		this.RedLayerMask = LayerMask.NameToLayer(this.RedLayerName);
		this.GreenLayerMask = LayerMask.NameToLayer(this.GreenLayerName);
		this.BlueLayerMask = LayerMask.NameToLayer(this.BlueLayerName);
		this.PlayerLayerMask = LayerMask.NameToLayer(this.PlayerLayerName);

		GameLevel._instance = this;

		Runity.Messenger.Reset();
		Runity.Messenger<GameColor>.Reset();
		Runity.Messenger<string>.Reset();


		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (player == null) {
			Debug.Log("GameLevel.Awake: Player object not found, creating it!");
			this.Player = GameObject.Instantiate(this.PlayerPrefab, this.PlayerSpawnPoint.position, Character_Motor.MODEL_3DSMAX)
				as GameObject;
		} else {
			this.Player = player;
		}
		this.Started = false;
		this.Finished = false;
		this.Paused = false;
	}

	private	void	Start() {
		//this.StartGame();
	}

	//public events

	public	void	StartGame() {
		this.Started = true;
		Runity.Messenger.Broadcast("Game.Start", Runity.MessengerMode.DONT_REQUIRE_LISTENER);
		Debug.Log("Game.Start");
	}

	public	void	EndGame() {
		this.Finished = true;
		Runity.Messenger.Broadcast("Game.End", Runity.MessengerMode.DONT_REQUIRE_LISTENER);
	}

	public	void	onPlayerWalkedOnCheckPoint(CheckPoint checkPoint) {
		if (this.lastCheckPoint && checkPoint.order <= this.lastCheckPoint.order) {
			return;
		}
		this.lastCheckPoint = checkPoint;
		Runity.Messenger<string>.Broadcast("Player.WalkedOnCheckPoint", checkPoint.gameObject.name,
		                                   Runity.MessengerMode.DONT_REQUIRE_LISTENER);
	}

	public	void	onPlayerWalkedOnDeadlyZone(DeadlyZone killer) {
		this.Player.GetComponent<ColorCharacterController>().Kill(killer);
		Runity.Messenger<string>.Broadcast("Player.Dead", killer.gameObject.name,
		                                   Runity.MessengerMode.DONT_REQUIRE_LISTENER);
		Transform playerRespawnPoint;
		if (this.lastCheckPoint == null) {
			playerRespawnPoint = this.PlayerSpawnPoint;
		} else {
			playerRespawnPoint = this.lastCheckPoint.transform;
		}
		this.Player.transform.position = playerRespawnPoint.position;
//		Camera.main.transform.position = new Vector3(this.Player.transform.position.x,
//		                                             this.Player.transform.position.y,
//		                                             Camera.main.transform.position.z);
	}

	public	void	onPlayerWalkedOnTrigger(SceneTrigger trigger) {
		if (trigger.Trigger()) {
			Runity.Messenger<string>.Broadcast("Player.WalkedOnTrigger", trigger.Label,
			                                   Runity.MessengerMode.DONT_REQUIRE_LISTENER);
		}
	}

	public bool IsPaused() {
		return this.Paused;
	}

	public void TogglePause() {
		if (this.Paused) {
			this.Resume();
		} else {
			this.Pause();
		}
	}

	public void Pause() {
		this.Paused = true;
		Time.timeScale = 0.0f;
		pauseScreen.SetActive(true);
	}

	public void Resume() {
		this.Paused = false;
		Time.timeScale = 1.0f;
		pauseScreen.SetActive(false);
	}

	public void ReturnMainMenu() {
		if (this.IsPaused()) {
		Application.LoadLevel("MainMenu");
		}
	}

	//private callbacks
}
