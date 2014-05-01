using UnityEngine;
using System.Collections;

public class ColorCharacterController : MonoBehaviour {
	
	//public attributes
	public	GameLevel.GameColor	StartColor = GameLevel.GameColor.Default;
	public	Material	playerMaterial;
	public	LayerMask	PlayerCollisionMask;
	public	Transform	PickObjectSpawn;

	//public properties
	public	IPickableObject	PickedObject { get; private set; }
	public	GameLevel.GameColor	CurrentColor { get; private set; }
	
	//private attributes
	private	LayerMask	defaultPlatformHit;
	
	//public methods
	public	void	ChangeColor(GameLevel.GameColor newColor) {
		if (this.CurrentColor == newColor) {
			return;
		}
		this.SetupColor(newColor);
	}
	
	public	void	Kill(DeadlyZone killer) {
		
	}

	public	void	PickObject(IPickableObject obj) {
		if (this.PickedObject != null) return;
		//		Runity.Messenger.Broadcast("Player.PickObject");
		obj.OnPick(this.gameObject);
		this.PickedObject = obj;
	}

	public	void	PickObject() {
		if (this.PickedObject != null) return;

		//for now dirty way
		Collider[] colls = Physics.OverlapSphere(this.transform.position, 2.0f);
		foreach (Collider coll in colls) {
			IPickableObject obj = (IPickableObject) coll.gameObject.GetComponent(typeof(IPickableObject));
			if (obj == null) continue;
			this.PickObject(obj);
		}
	}

	public	void	DropObject() {
		if (this.PickedObject == null) return;
		this.PickedObject.OnDrop(this.gameObject);
		this.PickedObject = null;
	}
	
	
	//private Unity methods
	private	void	Awake() {
		this.defaultPlatformHit = Character_Motor.Instance.SlidingLayerMask;
	}

	private	void	Start() {
		this.SetupColor(this.StartColor);
	}

	//private methods
	private	void	SetupPhysicsLayers(int newCollisionLayer, int layer1, int layer2) {
		Physics.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, newCollisionLayer, false);
		Physics.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, layer1, true);
		Physics.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, layer2, true);
		
		//special trick for Controller2D
		//if we change controller, remove this line
		Character_Motor.Instance.SlidingLayerMask = this.defaultPlatformHit | (1 << newCollisionLayer);
//		this.Controller2D.platformMask = this.defaultPlatformHit | (1 << newCollisionLayer);
	}

	private	void	SetupColor(GameLevel.GameColor newColor) {
		switch (newColor) {
		case GameLevel.GameColor.Blue:
			this.SetupPhysicsLayers(GameLevel.Instance.BlueLayerMask, GameLevel.Instance.RedLayerMask, GameLevel.Instance.GreenLayerMask);
//			playerMaterial.color = GameLevel.Instance.Blue;
			//			this.GetComponent<SpriteRenderer>().color = GameLevel.Instance.Blue;
			break;
		case GameLevel.GameColor.Red:
			this.SetupPhysicsLayers(GameLevel.Instance.RedLayerMask, GameLevel.Instance.GreenLayerMask, GameLevel.Instance.BlueLayerMask);
			//			this.GetComponent<SpriteRenderer>().color = GameLevel.Instance.Red;
//			playerMaterial.color = GameLevel.Instance.Red;
			break;
		case GameLevel.GameColor.Green:
			this.SetupPhysicsLayers(GameLevel.Instance.GreenLayerMask, GameLevel.Instance.RedLayerMask, GameLevel.Instance.BlueLayerMask);
			//			this.GetComponent<SpriteRenderer>().color = GameLevel.Instance.Green;
//			playerMaterial.color = GameLevel.Instance.Green;
			break;
		default:
			//physics
			Physics.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, GameLevel.Instance.RedLayerMask, true);
			Physics.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, GameLevel.Instance.GreenLayerMask, true);
			Physics.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, GameLevel.Instance.BlueLayerMask, true);
			//			this.Controller2D.platformMask = this.defaultPlatformHit;
			Character_Motor.Instance.SlidingLayerMask = this.defaultPlatformHit;
			
			//			this.GetComponent<SpriteRenderer>().color = Color.white;
			playerMaterial.color = GameLevel.Instance.White;
			break;
		}
		this.CurrentColor = newColor;
		Runity.Messenger<GameLevel.GameColor>.Broadcast("Player.ChangeColor", this.CurrentColor,
		                                                Runity.MessengerMode.DONT_REQUIRE_LISTENER);
	}
	
	
}