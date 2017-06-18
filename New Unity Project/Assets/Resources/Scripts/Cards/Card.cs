using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour {

	public int foodCost;
	public int foodConsumption;
	public int battleValue;
	public CardName name;

	public abstract void SpecialEffect();

	public void SetDraggable(bool flag) {
		if (this.GetComponent<Draggable>() != null)
			this.GetComponent<Draggable>().isActive = flag;
		else
			Debug.LogError("There should always be a Draggable script attached to a Card");
	}

	public enum CardName {
		SAMPLE_CARD,
		PEASANT_CARD
	};
		
	/*
	=====================
	GetPrefabPath
	=====================
	The name of the prefab and of the corresponding script should be the same !
	*/
	public string GetPrefabPath() {
		return "Prefabs/Cards/" + this.GetType().ToString();
	}
}