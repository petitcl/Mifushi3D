using UnityEngine;
using System.Collections;

	public static class GameObjectExtensionMethods {
		
		public	static	void		SetLayerRecursively(this GameObject go, int layer) {
			go.layer = layer;
			foreach (Transform child in go.transform) {
				child.gameObject.SetLayerRecursively(layer);
			}
		}
	}

