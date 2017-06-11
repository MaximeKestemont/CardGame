using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public ResourceManager resourceManager;
	public UIManager uiManager;

	private List<Player> playerList = new List<Player>();
	public Player activePlayer;

	public enum GamePhase {MULLIGAN, DRAW, AUTOMATIC_MAINTENANCE, 
		ACTIVE_MAINTENANCE_INVALID, ACTIVE_MAINTENANCE_VALID, MILITARY};
	public GamePhase currentPhase;

	void Awake() {
	}
		
	public void StartGame() {
		Debug.Log ("Starting the game...");
		resourceManager.menuPanel.SetActive(false);

		// Instantiate the players
		playerList.Add(new Player("Player1", 1, resourceManager.player1TextName, resourceManager.player1TextFood,
			resourceManager.player1TextAction, resourceManager.player1DeployementZone));
		playerList.Add(new Player("Player2", 2, resourceManager.player2TextName, resourceManager.player2TextFood,
			resourceManager.player2TextAction, resourceManager.player2DeployementZone));

		SetPlayerTurn(playerList[0]);

		// TODO temp code, should start with mulligan then military
		UpdateGamePhase(GamePhase.DRAW);
		ResolveDrawPhase();
	}


	/*
	=====================
	ResolveDrawPhase
	=====================
	1. Disable all cards except the hand
	2. Draw
	3. Get food
	4. Conquer territories
	*/
	public void ResolveDrawPhase() {
		if (currentPhase != GamePhase.DRAW)
			Debug.LogError("WRONG PHASE");

		// Cards outside of the hand should not be draggable for now
		activePlayer.SetDeploymentZoneCardInteractable(false);
		activePlayer.SetCampZoneCardInteractable(false);


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
		activePlayer.ComputeUnitsToKill();

		Debug.Log("Automatic Maintenance phase : finished ");
		UpdateGamePhase(GamePhase.ACTIVE_MAINTENANCE_INVALID);
		ActiveMaintenancePhase();

		// TODO Should slow and animate the removal of food
	}

	/*
	=====================
	ActiveMaintenancePhase
	=====================
	// Start in invalid phase, and will only be valid once all units that should be killed are killed
	*/
	public void ActiveMaintenancePhase() {
		if (currentPhase != GamePhase.ACTIVE_MAINTENANCE_INVALID)
			Debug.LogError("WRONG PHASE");
		Debug.Log("Active Maintenance phase : start");

		bool isValid = true; // false if the player cannot go to the next phase

		foreach (KeyValuePair<DeploymentZone, int> k in activePlayer.unitsToKill) {
			DeploymentZone zone = k.Key;

			if (k.Value > 0) {
				isValid = false;

				// Hightlight the zone to indicate there is something to do
				zone.Highlight();

				zone.SetCardInteractable(true);

				// Display a pop-up
				// TODO

			} else {
				// The player cannot interact with those cards in this phase
				zone.SetCardInteractable(false);
			}
		}

		if (isValid) {
			Debug.Log("Active Maintenance phase : finished");
			UpdateGamePhase(GamePhase.ACTIVE_MAINTENANCE_VALID);
			EndMaintenancePhase();
		}
	}

	/*
	=====================
	EndMaintenancePhase
	=====================
	*/
	// TODO delete this phase ? 
	public void EndMaintenancePhase() {
		if (currentPhase != GamePhase.ACTIVE_MAINTENANCE_VALID)
			Debug.LogError("WRONG PHASE");

		UpdateGamePhase(GamePhase.MILITARY);
		ResolveMilitaryPhase();
	}


	/*
	=====================
	ResolveMilitaryPhase
	=====================
	*/
	public void ResolveMilitaryPhase() {
		if (currentPhase != GamePhase.MILITARY)
			Debug.LogError("WRONG PHASE");

		// Set the player actions to 3
		activePlayer.SetActionCounter(3);


		// TODO continue here
		// First : display the number of actions left to do.
		// Then, render all cards interactable (except those in the graveyard)
		// 3 actions to do, among : 
		// 	1. play a card
		// 	2. move a deployed card
		//  3. deploy cards
		//	4. get food
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


// TODO to move elsewhere?
public static class Utils {

	public static string ToDebugString<TKey, TValue> (this IDictionary<TKey, TValue> dictionary)
	{
		string result = "";
		foreach (KeyValuePair<TKey, TValue> kv in dictionary) {
			string s = "(" + kv.Key.ToString() + " = " + kv.Value.ToString() + "),"; 
			result += s;
		}
		return result;
	}

}