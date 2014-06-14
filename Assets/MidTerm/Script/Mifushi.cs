using UnityEngine;
using System.Collections;

public class Mifushi : MonoBehaviour {

	static public SoundManager SoundManagerInst {
		get; private set;
	}

	public SoundManager SoundManagerPrefab;

	void Awake () {
		if (Mifushi.SoundManagerInst == null) {
			Mifushi.SoundManagerInst = GameObject.Instantiate(SoundManagerPrefab) as SoundManager;
			Object.DontDestroyOnLoad(Mifushi.SoundManagerInst);
		}
	}

}
