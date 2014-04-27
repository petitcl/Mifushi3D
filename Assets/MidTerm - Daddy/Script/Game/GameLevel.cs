using UnityEngine;
using System.Collections;

public class GameLevel : MonoBehaviour {

	//static attributes
	private	static	GameLevel	_instance = null;
	public	static	GameLevel	Instance {
		get {
			if (GameLevel._instance == null) {
				GameLevel._instance = new GameLevel();
			}
			return GameLevel._instance;
		}
	}
	//public types
	public	enum GameColor {
		Red,
		Green,
		Blue,
		Default
	}

	//public attributes
	public	Color	Red;
	public	Color	Green;
	public	Color	Blue;
	
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


		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		if (player == null) {
			this.Player = GameObject.Instantiate(this.PlayerPrefab, this.PlayerSpawnPoint.position, Character_Motor.MODEL_3DSMAX)
				as GameObject;
		} else {
			this.Player = player;
		}
	}

	private	void	Start() {
		Runity.Messenger.Broadcast("Game.Start", Runity.MessengerMode.DONT_REQUIRE_LISTENER);
	}

	//public events
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
		Runity.Messenger<string>.Broadcast("Player.WalkedOnTrigger", trigger.Label,
		                                   Runity.MessengerMode.DONT_REQUIRE_LISTENER);
	}

	//private callbacks
}
