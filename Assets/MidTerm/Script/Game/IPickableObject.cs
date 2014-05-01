using UnityEngine;
using System.Collections;

public interface IPickableObject {

	bool	Pickable { get; set; }
	bool	Dropable { get; set; }

	void	OnPick(GameObject picker);
	void	OnDrop(GameObject picker);

}
