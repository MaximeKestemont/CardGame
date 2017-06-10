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

	// this is used to temporarily store the units to kill in each deployment zone during the maintenance phase
	public Dictionary<DeploymentZone, int> unitsToKill = new Dictionary<DeploymentZone, int>();

	private Color normalColor;
	private bool isActive;

	private GameManager gm;

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

		// TODO should be retrieved in a safer way
		// currently only needed to get the currentStatus
		gm = GameObject.Find("GameManager").GetComponent<GameManager>(); 

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
	Check that enough food/units were consumed/killed in each deployment zone
	If this is the case, then the player can move to the next phase
	*/
	public void MaintenanceCheck() {
		//Dictionary<DeploymentZone, int> unitsToKill = new Dictionary<DeploymentZone, int>();
		int count = 0;
		foreach (KeyValuePair<DeploymentZone, int> kv in unitsToKill) {
			count += Mathf.Max(0, kv.Value); // should never be negative in theory
		}
		if (count > 0) {
			gm.currentPhase = GameManager.GamePhase.ACTIVE_MAINTENANCE_INVALID;
		} else {
			gm.currentPhase = GameManager.GamePhase.ACTIVE_MAINTENANCE_VALID;
			Debug.Log("Active Maintenance phase : finished");
			gm.EndMaintenancePhase();
		}
	}

	/*
	=====================
	ComputeUnitsToKill
	=====================
	*/
	public void ComputeUnitsToKill() {
		unitsToKill.Clear();
		foreach (KeyValuePair<DeploymentZone.ZonePosition, DeploymentZone> d in deployementZoneMap) {
			unitsToKill.Add(d.Value, d.Value.ConsumeFood());
		}
	}

	/*
	=====================
	RemoveUnitToKill
	=====================
	*/
	public void RemoveUnitToKill(DeploymentZone zone, int number) {
		int value;
		if (unitsToKill.TryGetValue(zone, out value)) {
			unitsToKill[zone] = Mathf.Max(0, value - number); // should never be below 0 in theory
			if (unitsToKill[zone] == 0)
				zone.StopHighlight();
		} else {
			Debug.LogError("The zone does not exist !");
		}
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
