using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {

	public Button button;
	public Text buttonText;
	private GameControllerTicTac gameController;


	public void SetSpace () {
		buttonText.text = gameController.GetPlayerSide();
		button.interactable = false;
		gameController.EndTurn();
	}

	public void SetGameControllerReference (GameControllerTicTac controller) {
		gameController = controller;
	}

}
