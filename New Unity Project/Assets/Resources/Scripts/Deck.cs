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
		UpdateCardText();
	}

	public void UpdateCardText() {
		nbCardsText.text = cardList.Count.ToString();
	}

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

	public void Randomize() {
		// TODO
	}
}
