using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public ResourceManager resourceManager;

	private List<Player> playerList = new List<Player>();
	private int activePlayer = 1;

	public enum TurnStatus {PHASE1, PHASE2, PHASE3}; // TODO to use later
	// TurnStatus status = TurnStatus.PHASE1;

	void Awake() {
	}
		
	public void StartGame() {
		Debug.Log ("Starting the game...");
		resourceManager.menuPanel.SetActive(false);

		// Instantiate the players
		playerList.Add(new Player("Player1", 1, resourceManager.player1Text));
		playerList.Add(new Player("Player2", 2, resourceManager.player2Text));

		setPlayerTurn(1);
	}


	/*
	=====================
	setPlayerTurn
	=====================
	*/
	public void setPlayerTurn(int playerNumber) {
		playerList[activePlayer - 1].SetActive(false); // previous player is now inactive
		playerList[playerNumber - 1].SetActive(true);
		activePlayer = playerNumber;
	}

}
