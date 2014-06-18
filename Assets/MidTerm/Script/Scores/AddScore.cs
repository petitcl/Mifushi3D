using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TextInput l_username = gameObject.GetComponent<TextInput> ();
		if (l_username == null)
			return;
		l_username.OnValidate += this.CastEvent;
	}
	
	// Update is called once per frame
	void Update () {

	}
	void CastEvent(string username) {
		Debug.Log ("AddScore.CastEvent");
		Runity.Messenger<string>.Broadcast("Game.SetUsername", username, Runity.MessengerMode.DONT_REQUIRE_LISTENER);
	}
}
