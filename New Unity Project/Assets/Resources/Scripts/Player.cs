using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player {

	public string playerName;

	private int playerNumber;
	private Text playerText;
	private int foodNumber;
	private Text foodText;
	public List<DeploymentZone> deployementZoneList = new List<DeploymentZone>();

	private Color normalColor;

	private bool isActive;

	public Player(
		string name, 
		int playerNumber, 
		Text playerText, 
		Text foodText,
		List<DeploymentZone> deployementZoneList) 
	{
		this.playerName = name;
		this.playerNumber = playerNumber;
		this.playerText = playerText;
		this.foodText = foodText;
		this.deployementZoneList = deployementZoneList;

		playerText.text = playerName;
		isActive = false;
		normalColor = new Color(1, 1, 1, 1);

		// Initialize food 
		SetFoodNumber(3);
	}




	// TODO maybe move this in a future Deck.cs script ? 
	public void DrawCard(int number) {
		// TODO
	}

	/*
	=====================
	addFood
	=====================
	*/
	public void AddFood(int number) {
		SetFoodNumber(foodNumber + number);
	}

	/* ---------------------------- Getter / Setter -------------------------- */

	public void SetFoodNumber(int number) {
		foodNumber = number;
		foodText.text = "Food : " + number;
	}

	public int GetFoodNumber() {
		return foodNumber;
	}

	public void SetActive(bool flag) {
		if (flag == true)
			playerText.color = new Color(255, 0, 0, 255);
		else
			playerText.color = normalColor;

		isActive = flag;
	}
		
}
