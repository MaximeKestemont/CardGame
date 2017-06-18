using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCard : Card {

	public SampleCard() {
		foodCost = 1;
		foodConsumption = 1;
		battleValue = 2;
		name = CardName.SAMPLE_CARD;
	}

	public override void SpecialEffect() {
	}
}