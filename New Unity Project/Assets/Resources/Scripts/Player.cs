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
	public Dictionary<DeploymentZone.ZonePosition, DeploymentZone> deployementZoneMap = new Dictionary<DeploymentZone.ZonePosition, DeploymentZone>();

	private Color normalColor;

	private bool isActive;

	public Player(
		string name, 
		int playerNumber, 
		Text playerText, 
		Text foodText,
		Dictionary<DeploymentZone.ZonePosition, DeploymentZone> deployementZoneMap) 
	{
		this.playerName = name;
		this.playerNumber = playerNumber;
		this.playerText = playerText;
		this.foodText = foodText;
		this.deployementZoneMap = deployementZoneMap;

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
	MaintenanceCheck
	=====================
	// Check that enough food/units were consumed/killed in each deployment zone
	// If this is the case, then the player can move to the next phase
	*/
	public void MaintenanceCheck() {
		// TODO
	}

	/*
	=====================
	AddFood
	=====================
	*/
	public void AddFood(int number) {
		SetFoodNumber(foodNumber + number);
	}

	/*
	=====================
	SetDeploymentZoneCardInteractable
	=====================
	*/
	public void SetDeploymentZoneCardInteractable(bool flag) {
		foreach (KeyValuePair<DeploymentZone.ZonePosition, DeploymentZone> kv in deployementZoneMap) {
			kv.Value.SetCardInteractable(flag);
		}
	}

	/*
	=====================
	SetCampZoneCardInteractable
	=====================
	*/
	public void SetCampZoneCardInteractable(bool flag) {
		// TODO
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
