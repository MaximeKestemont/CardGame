using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Zone where a player can deploy cards
public class DeploymentZone : MonoBehaviour {

	public enum ZonePosition {LEFT, MIDDLE, RIGHT};

	public List<Card> cardList = new List<Card>();
	public int foodNumber = 0;
	public int battleValue = 0;
	public ZonePosition zonePosition;

	public Text battleValueText;	// TODO replace with symbols

	public List<Object> foodIconList = new List<Object>();

	public void Awake() {
		SetBattleValue(0);
		UpdateFoodDisplay();

	}


	public void SetBattleValue(int number) {
		battleValue = number;
		battleValueText.text = number.ToString();
	}

	/*
	=====================
	UpdateFoodDisplay
	=====================
	Does not update foodNumber, only affect the UI and Icon list !
	*/
	public void UpdateFoodDisplay() {		
		int foodDiff = foodIconList.Count - foodNumber;

		// Remove excedent food icons
		for (int i = 0; i < foodDiff; ++i) {
			// TODO is there not a easier way to remove from list + destroy an object ? 
			Object food = foodIconList[0];
			foodIconList.Remove(food);
			Destroy(food);
		}

		// Add missing food icons
		for (int i = 0 ; i > foodDiff ; --i) {
			Object food = Instantiate(Resources.Load("Prefabs/Food"), transform);
			foodIconList.Add(food);
		}
	}

	/*
	=====================
	ConsumeFood
	=====================
	Return the number of units to kill in the zone because of too few food
	*/
	public int ConsumeFood() {
		int foodToConsume = 0;
		foreach (Card c in cardList) {
			foodToConsume += c.foodConsumption;
		}
			
		int foodLeft = foodNumber - foodToConsume;
		foodNumber = Mathf.Max(0, foodLeft);
		UpdateFoodDisplay();

		if (foodLeft < 0)
			return foodLeft * -1;
		else
			return 0;
	}

	/*
	=====================
	SetCardInteractable
	=====================
	Set the interactibility of the cards contained in the zone
	*/
	public void SetCardInteractable(bool flag) {
		foreach (Card c in cardList) {
			c.SetDraggable(flag);
		}
	}

	/*
	=====================
	Highlight
	=====================
	Set the interactibility of the cards contained in the zone
	*/
	public void Highlight() {
		// TODO should make a red border instead of changing the color of the zone
		this.GetComponent<Image>().color = new Color(255, 0, 0);
	}

	/*
	=====================
	StopHighlight
	=====================
	Set the interactibility of the cards contained in the zone
	*/
	public void StopHighlight() {
		// TODO should remove the red border here
		this.GetComponent<Image>().color = new Color(255, 255, 255, 100); 
	}

	/*
	=====================
	RemoveCard
	=====================
	*/
	public void RemoveCard(Draggable d) {
		Debug.Log(cardList.Count);
		Card c = d.GetComponent<Card>();
		if (c == null) {
			Debug.LogError("Trying to remove a draggable not attached to a card");
		} else {
			cardList.Remove(c);
		}
	}


}
