using UnityEngine;
using System.Collections;

public interface IPickableObject {

	void	OnPick(GameObject picker);
	void	OnDrop(GameObject picker);

}
