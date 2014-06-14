using UnityEngine;
using System.Collections;

public class GameLevel : Runity.MonoBehaviourExt {

	//public types
	public	enum GameColor {
		Red,
		Green,
		Blue,
		Default
	}

	//static attributes
	private	static	GameLevel	_instance = null;
	//TODO : refactor this in public static GameLevel xxx {get; priv set; } everywhere
	public	static	GameLevel	Instance {
		get {
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

	public	IGameScene	GameScene;

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

	public	float	TimeSinceStart { get; private set; }

	//private attributes
	private CheckPoint	lastCheckPoint;


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
		Runity.Messenger<int>.Reset();
		Runity.Messenger<GameObject>.Reset();


		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (player == null) {
			Debug.Log("GameLevel.Awake: Player object not found, creating it!");
			this.Player = GameObject.Instantiate(this.PlayerPrefab, this.PlayerSpawnPoint.position, Character_Motor.MODEL_3DSMAX)
				as GameObject;
		} else {
			this.Player = player;
			this.Player.transform.position = this.PlayerSpawnPoint.position;
		}
		this.Player.GetComponent<Character_Manager>().CanMove = false;
		this.Started = false;
		this.Finished = false;
		this.Paused = false;

		this.TimeSinceStart = 0.0f;
		Time.timeScale = 1.0f;
	}

	private	void	Start() {
		this.Started = false;
		this.Finished = false;
		this.Paused = false;
		if (this.GameScene == null) GameAnimator.Instance.PlayAnimation("Game.Start", this.onStartAnimationDone);
	}

	//public methods
	public	void	StartGame() {
		if (this.Started) return;
		this.Started = true;



		this.Player.GetComponent<Character_Manager>().CanMove = true;

		Runity.Messenger.Broadcast("Game.Start", Runity.MessengerMode.DONT_REQUIRE_LISTENER);
	}

	public	void	EndGame() {
		if (this.Finished) return;
		this.Finished = true;

		this.Player.GetComponent<Character_Manager>().CanMove = false;

		Runity.Messenger.Broadcast("Game.End", Runity.MessengerMode.DONT_REQUIRE_LISTENER);
		GameAnimator.Instance.PlayAnimation("Game.End");

		Debug.Log("Game.End");
	}

	public	void	onPlayerWalkedOnCheckPoint(CheckPoint checkPoint) {
		if (this.lastCheckPoint && checkPoint.order <= this.lastCheckPoint.order) {
			return;
		}
		SoundManager.Instance.Play(SoundManager.GameEvent.NewCheckpoint);
		this.lastCheckPoint = checkPoint;
		Runity.Messenger<int>.Broadcast("Player.WalkedOnCheckPoint", checkPoint.order,
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
		this.Player.GetComponent<Character_Manager>().ResetSpeed();
	}

	public	void	onPlayerWalkedOnTrigger(SceneTrigger trigger) {
		if (trigger.Trigger()) {
			Runity.Messenger<string>.Broadcast("Player.WalkedOnTrigger", trigger.Label,
			                                   Runity.MessengerMode.DONT_REQUIRE_LISTENER);
		}
	}

	public void TogglePause() {
		if (this.Paused) {
			this.Resume();
		} else {
			this.Pause();
		}
	}

	public void Pause() {
		if (this.Paused || !this.Started || this.Finished) return;
		this.Paused = true;
		Time.timeScale = 0.0f;
		Runity.Messenger.Broadcast("Game.Pause",
		                             Runity.MessengerMode.DONT_REQUIRE_LISTENER);
	}

	public void Resume() {
		if (!this.Paused) return;
		this.Paused = false;
		Time.timeScale = 1.0f;
		Runity.Messenger.Broadcast("Game.Resume",
		                           Runity.MessengerMode.DONT_REQUIRE_LISTENER);
	}

	public void ReturnMainMenu() {
		if (this.Paused || this.Finished) {
//			GameLevel._instance = null;
//			GameObject.DestroyImmediate(this.gameObject);
			Application.LoadLevel("MainMenu");
		}
	}

	public	int		GameColorToLayerMask(GameColor color) {
		switch (color) {
		case GameColor.Red:
			return this.RedLayerMask;
		case GameColor.Green:
			return this.GreenLayerMask;
		case GameColor.Blue:
			return this.BlueLayerMask;
		default:
			//TODO : fix this thing (flemme)
			return this.PlayerLayerMask;
		}
	}

	public	Color	GameColorToFadedColor(GameColor color) {
		GameColor currentColor = this.Player.GetComponent<ColorCharacterController>().CurrentColor;
		switch (color) {
		case GameColor.Red:
			if (currentColor == GameColor.Red) return this.Red;
			else return this.FadedRed;
		case GameColor.Green:
			if (currentColor == GameColor.Green) return this.Green;
			else return this.FadedGreen;
		case GameColor.Blue:
			if (currentColor == GameColor.Blue) return this.Blue;
			else return this.FadedBlue;
		default:
			return this.White;
		}
	}

	public	Color	GameColorToColor(GameColor color) {
		switch (color) {
		case GameColor.Red:
			return this.Red;
		case GameColor.Green:
			return this.Green;
		case GameColor.Blue:
			return this.Blue;
		default:
			return this.White;
		}
	}

	//private Unity callbacks
	private	void	Update() {
		if (this.Running) this.TimeSinceStart += Time.deltaTime;
	}

	//private callbacks
	private	void	onStartAnimationDone() {
		this.StartGame();
	}
}
