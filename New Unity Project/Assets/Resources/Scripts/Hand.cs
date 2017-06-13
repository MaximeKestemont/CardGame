using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Zone {

	void Awake() {
		type = ZoneType.HAND;
	}

	/*
	=====================
	InstantiateNewCard
	=====================
	*/
	public void InstantiateNewCard(Card c) {
		Object cardObject = Instantiate(Resources.Load("Prefabs/Card"), transform);
		Card newCard = cardObject as Card;
		cardList.Add(newCard);
	}
}
