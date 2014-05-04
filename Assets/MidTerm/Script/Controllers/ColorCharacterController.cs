using UnityEngine;
using System.Collections;

public class ColorCharacterController : MonoBehaviour {
	
	//public attributes
	public	GameLevel.GameColor	StartColor = GameLevel.GameColor.Default;
	public	Material	playerMaterial;
	public	LayerMask	PlayerCollisionMask;
	public	Transform	PickObjectSpawn;
	public	float		PushPower = 5.0f;
	public	CharacterController charCtrl;

	//public properties
	public	GameObject	PickedObject { get; private set; }
	public	GameLevel.GameColor	CurrentColor { get; private set; }
	
	//private attributes
	private	LayerMask	defaultPlatformHit;
	
	//public methods
	public	void	ChangeColor(GameLevel.GameColor newColor) {
		if (this.CurrentColor == newColor) {
			return;
		}
		this.SetupColor(newColor);
		//Tick to generate collision detection
		charCtrl.Move(Vector3.forward * 0.00001f);
	}
	
	public	void	Kill(DeadlyZone killer) {
		
		SoundManager.Instance.Play(SoundManager.GameEvent.PlayerDie);
	}

	public	void	PickObject(GameObject go) {
		if (this.PickedObject != null) return;

		IPickableObject obj = (IPickableObject) go.GetComponent(typeof(IPickableObject));
		if (obj == null) return;
		if (!obj.Pickable) return;

		obj.OnPick(this.gameObject);
		this.PickedObject = go;

		Runity.Messenger<GameObject>.Broadcast("Player.PickedObject", go, Runity.MessengerMode.DONT_REQUIRE_LISTENER);
		SoundManager.Instance.Play(SoundManager.GameEvent.PickObject);
	}

	public	void	PickObject() {
		Debug.Log("PickObject");
		if (this.PickedObject != null) return;

		//for now dirty way
		Collider[] colls = Physics.OverlapSphere(this.transform.position, 2.0f);
		foreach (Collider coll in colls) {
			this.PickObject(coll.gameObject);
		}
	}

	public	void	DropObject() {
		Debug.Log("DropObject");
		if (this.PickedObject == null) return;
		IPickableObject obj = (IPickableObject) this.PickedObject.GetComponent(typeof(IPickableObject));
		if (obj == null) return;
		if (!obj.Dropable) return;
		obj.OnDrop(this.gameObject);
		Runity.Messenger<GameObject>.Broadcast("Player.DroppedObject", this.PickedObject, Runity.MessengerMode.DONT_REQUIRE_LISTENER);
		this.PickedObject = null;
		SoundManager.Instance.Play(SoundManager.GameEvent.DropObject);
	}
	
	
	//private Unity methods
	private	void	Awake() {
		this.defaultPlatformHit = Character_Motor.Instance.SlidingLayerMask;
	}

	private	void	Start() {
		this.SetupColor(this.StartColor);
		charCtrl = this.GetComponent<CharacterController>();
	}

	private	void	OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "Terrain") {
			GameLevel.Instance.onPlayerWalkedOnDeadlyZone(hit.gameObject.GetComponent<DeadlyZone>());
		}
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;
		
		if (hit.moveDirection.y < -0.3F)
			return;
		
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		body.AddForce(pushDir * this.PushPower);
	}

	//private methods
	private	void	SetupPhysicsLayers(int newCollisionLayer, int layer1, int layer2) {
		Physics.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, newCollisionLayer, false);
		Physics.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, layer1, true);
		Physics.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, layer2, true);
		
		//if we change controller, remove this line
		Character_Motor.Instance.SlidingLayerMask = this.defaultPlatformHit | (1 << newCollisionLayer);
	}

	private	void	SetupColor(GameLevel.GameColor newColor) {
		switch (newColor) {
		case GameLevel.GameColor.Blue:
			this.SetupPhysicsLayers(GameLevel.Instance.BlueLayerMask, GameLevel.Instance.RedLayerMask, GameLevel.Instance.GreenLayerMask);
			break;
		case GameLevel.GameColor.Red:
			this.SetupPhysicsLayers(GameLevel.Instance.RedLayerMask, GameLevel.Instance.GreenLayerMask, GameLevel.Instance.BlueLayerMask);
			break;
		case GameLevel.GameColor.Green:
			this.SetupPhysicsLayers(GameLevel.Instance.GreenLayerMask, GameLevel.Instance.RedLayerMask, GameLevel.Instance.BlueLayerMask);
			break;
		default:
			//physics
			Physics.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, GameLevel.Instance.RedLayerMask, true);
			Physics.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, GameLevel.Instance.GreenLayerMask, true);
			Physics.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, GameLevel.Instance.BlueLayerMask, true);
			Character_Motor.Instance.SlidingLayerMask = this.defaultPlatformHit;
			
			playerMaterial.color = GameLevel.Instance.White;
			break;
		}
		this.CurrentColor = newColor;
		Runity.Messenger<GameLevel.GameColor>.Broadcast("Player.ChangeColor", this.CurrentColor,
		                                                Runity.MessengerMode.DONT_REQUIRE_LISTENER);
	}
	
	
}