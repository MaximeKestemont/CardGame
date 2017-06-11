using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Zone where a player can deploy cards
public class DeploymentZone : Zone {

	public enum ZonePosition {LEFT, MIDDLE, RIGHT};

	//public List<Card> cardList = new List<Card>();
	public int foodNumber = 0;
	public int battleValue = 0;
	public ZonePosition zonePosition;

	public Text battleValueText;	// TODO replace with symbols

	public List<Object> foodIconList = new List<Object>();

	// number of cards deployed in this zone during this turn
	public int nbDeployedHere = 0;

	public void Awake() {
		normalColor = this.GetComponent<Image>().color;
		SetBattleValue(0);
		UpdateFoodDisplay();
		type = ZoneType.DEPLOYMENT_ZONE;
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
	IsNeighbour
	=====================
	*/
	public bool IsNeighbour(DeploymentZone zone) {
		switch (this.zonePosition) {

		case ZonePosition.LEFT:
			if (zone.zonePosition == ZonePosition.MIDDLE)
				return true;
			else
				return false;
			break;

		case ZonePosition.MIDDLE:
			if (zone.zonePosition == ZonePosition.MIDDLE)
				return false;
			else
				return true;
			break;

		case ZonePosition.RIGHT:
			if (zone.zonePosition == ZonePosition.MIDDLE)
				return true;
			else
				return false;
			break;
		}
		return false; // should never be reached
	}

}
