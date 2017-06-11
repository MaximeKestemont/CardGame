using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Button actionButton;
	private Text actionButtonText;

	public void Awake() {
		actionButtonText = actionButton.GetComponentInChildren<Text>();
	}

	public void UpdateActionButton(GameManager.GamePhase gamePhase) {
		actionButtonText.text = gamePhase.ToString();

		switch (gamePhase) {
		case GameManager.GamePhase.MULLIGAN:
			actionButton.interactable = true;
			actionButtonText.text = "Finish mulligan";
			break;
		case GameManager.GamePhase.DRAW:
			actionButton.interactable = false;
			break;
		case GameManager.GamePhase.AUTOMATIC_MAINTENANCE:
			actionButton.interactable = false;
			break;
		case GameManager.GamePhase.ACTIVE_MAINTENANCE_INVALID:
			actionButton.interactable = false;
			actionButtonText.text = "Finish maintenance";
			break;
		case GameManager.GamePhase.ACTIVE_MAINTENANCE_VALID:
			actionButton.interactable = false;
			actionButtonText.text = "Finish maintenance";
			break;
		case GameManager.GamePhase.MILITARY:
			actionButton.interactable = true;
			actionButtonText.text = "Finish turn";
			break;
		}
	}
}
