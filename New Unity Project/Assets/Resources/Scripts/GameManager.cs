﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public ResourceManager resourceManager;
	public UIManager uiManager;

	private List<Player> playerList = new List<Player>();
	private Player activePlayer;

	public enum GamePhase {MULLIGAN, DRAW, AUTOMATIC_MAINTENANCE, ACTIVE_MAINTENANCE_INVALID, ACTIVE_MAINTENANCE_VALID, MILITARY};
	public GamePhase currentPhase;

	void Awake() {
	}
		
	public void StartGame() {
		Debug.Log ("Starting the game...");
		resourceManager.menuPanel.SetActive(false);

		// Instantiate the players
		playerList.Add(new Player("Player1", 1, resourceManager.player1TextName, resourceManager.player1TextFood,
			resourceManager.player1DeployementZone));
		playerList.Add(new Player("Player2", 2, resourceManager.player2TextName, resourceManager.player2TextFood,
			resourceManager.player2DeployementZone));

		SetPlayerTurn(playerList[0]);

		// TODO temp code, should start with mulligan then military
		UpdateGamePhase(GamePhase.DRAW);
		ResolveDrawPhase();
	}


	/*
	=====================
	ResolveDrawPhase
	=====================
	1. Draw
	2. Get food
	3. Conquer territories
	*/
	public void ResolveDrawPhase() {
		if (currentPhase != GamePhase.DRAW)
			Debug.LogError("WRONG PHASE");
		
		activePlayer.DrawCard(2);
		activePlayer.AddFood(2);

		// TODO RESOLVE THE CONQUEST OF TERRITORIES HERE

		// Phase finished -> go to maintenance phase
		Debug.Log("Draw phase : finished");
		UpdateGamePhase(GamePhase.AUTOMATIC_MAINTENANCE);
		AutomaticMaintenancePhase();

		// TODO should slow and animate the drawing + food addition
	}

	/*
	=====================
	AutomaticMaintenancePhase
	=====================
	1. Resolve special interactions (goat, etc.) (TODO later on)
	2. Resolve automatic maintenance
	*/
	public void AutomaticMaintenancePhase() {
		if (currentPhase != GamePhase.AUTOMATIC_MAINTENANCE)
			Debug.LogError("WRONG PHASE");
		Debug.Log("Automatic Maintenance phase : start");

		// For each deployment zone, get the number of units to kill
		Dictionary<DeploymentZone.ZonePosition, int> unitsToKill = new Dictionary<DeploymentZone.ZonePosition, int>();
		foreach (KeyValuePair<DeploymentZone.ZonePosition, DeploymentZone> d in activePlayer.deployementZoneMap) {
			unitsToKill.Add(d.Key, d.Value.ConsumeFood());
		}

		Debug.Log("Automatic Maintenance phase : finished ");
		UpdateGamePhase(GamePhase.ACTIVE_MAINTENANCE_INVALID);
		ActiveMaintenancePhase(unitsToKill);

		// TODO Should slow and animate the removal of food
	}

	/*
	=====================
	ActiveMaintenancePhase
	=====================
	// Start in invalid phase, and will only be valid once all units that should be killed are killed
	*/
	public void ActiveMaintenancePhase(Dictionary<DeploymentZone.ZonePosition, int> unitsToKill) {
		if (currentPhase != GamePhase.ACTIVE_MAINTENANCE_INVALID)
			Debug.LogError("WRONG PHASE");
		Debug.Log("Active Maintenance phase : start");

		bool isValid = false; // false if the player cannot go to the next phase
		foreach (KeyValuePair<DeploymentZone.ZonePosition, int> k in unitsToKill) {
				// TODO 
		}

		Debug.Log(unitsToKill[DeploymentZone.ZonePosition.MIDDLE]);
	}

	/*
	=====================
	EndMaintenancePhase
	=====================
	3. Resolve player maintenance choice
	*/
	public void EndMaintenancePhase() {
		// TODO resolve the maintenance selected by the player and move to the next phase
	}




	/*
	=====================
	ResolveActionPhase
	=====================
	*/
	public void ResolveActionPhase() {
		// TODO
	}



	/*
	=====================
	UpdateGamePhase
	=====================
	*/
	public void UpdateGamePhase(GamePhase gamePhase) {
		this.currentPhase = gamePhase;
		uiManager.UpdateActionButton(gamePhase);
	}

	/*
	=====================
	SetPlayerTurn
	=====================
	*/
	public void SetPlayerTurn(Player player) {
		foreach (Player p in playerList) {
			p.SetActive(false);
		}
		player.SetActive(true);
		activePlayer = player;
	}

}
