using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour {

	public Text nbCardsText;
	private List<Card> cardList = new List<Card>(); // TODO make it private at one point, for security reason?

	void Awake() {
		Card c = new SampleCard();
		cardList.Add(c);
		Card c1 = new SampleCard();
		cardList.Add(c1);
		Card c2 = new PeasantCard();
		cardList.Add(c2);
		Card c3 = new PeasantCard();
		cardList.Add(c3);
		UpdateCardText();
		Suffle();
	}

	public void UpdateCardText() {
		nbCardsText.text = cardList.Count.ToString();
	}

	/*
	=====================
	PickCard
	=====================
	Pick the first card of the deck (no shuffling done)
	*/
	public void PickCard(Player player) {
		if (cardList.Count > 0) {
			Card c = cardList[0];
			cardList.Remove(c);
			player.hand.InstantiateNewCard(c);
			UpdateCardText();
		} else {
			Debug.Log("No more card in the deck !");
			// TODO what do to when no more card? 
		}
	}

	/*
	=====================
	Suffle
	=====================
	Perfect shuffle of the deck
	*/
	public void Suffle() {
		for (int i = cardList.Count - 1; i >= 0 ; --i) {
			int j = Random.Range(0, i);
			Card temp = cardList[i];
			cardList[i] = cardList[j];
			cardList[j] = temp;
		}
	}
}
