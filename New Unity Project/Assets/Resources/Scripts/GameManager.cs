using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public ResourceManager resourceManager;

	private List<Player> playerList = new List<Player>();
	private Player activePlayer;

	public enum GamePhase {MULLIGAN, DRAW, MAINTENANCE, MILITARY};
	public GamePhase currentPhase;

	void Awake() {
	}
		
	public void StartGame() {
		Debug.Log ("Starting the game...");
		resourceManager.menuPanel.SetActive(false);

		// Instantiate the players
		playerList.Add(new Player("Player1", 1, resourceManager.player1TextName, resourceManager.player1TextFood));
		playerList.Add(new Player("Player2", 2, resourceManager.player2TextName, resourceManager.player2TextFood));

		SetPlayerTurn(playerList[0]);

		// TODO temp code, should start with mulligan then military
		currentPhase = GamePhase.DRAW;
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
		currentPhase = GamePhase.MAINTENANCE;
	}

	/*
	=====================
	StartMaintenancePhase
	=====================
	1. Resolve special interactions (goat, etc.)
	2. Resolve automatic maintenance
	*/
	public void StartMaintenancePhase() {
		// TODO resolve the automatic maintenance + special interaction, and then give the hand to the player to resolve the rest
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
	setPlayerTurn
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
