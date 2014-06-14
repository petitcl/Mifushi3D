using UnityEngine;
using System.Collections;

public class Mifushi : MonoBehaviour {

	static public SoundManager SoundManagerInst {
		get; private set;
	}

	public SoundManager SoundManagerPrefab;

	void Awake () {
		if (Mifushi.SoundManagerInst == null) {
			Mifushi.SoundManagerInst = GameObject.Instantiate(SoundManagerPrefab);
		}
	}

	// Use this for initialization
	void Start () {
		SoundManagerInst.Play(
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
