using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantCard : Card {

	public PeasantCard() {
		foodCost = 0;
		foodConsumption = 1;
		battleValue = 1;
		name = CardName.PEASANT_CARD;
	}

	public override void SpecialEffect() {
	}
}