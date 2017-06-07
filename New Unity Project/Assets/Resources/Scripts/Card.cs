using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour {

	public int foodCost;
	public int foodConsumption;
	public int battleValue;

	public abstract void SpecialEffect();
}

// TODO add class declaration here or in another file
public class CardExample : Card {

	public CardExample() {
		foodCost = 1;
		foodConsumption = 1;
		battleValue = 2;
	}

	public override void SpecialEffect() {
	}
}