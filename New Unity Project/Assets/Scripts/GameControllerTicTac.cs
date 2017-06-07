using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerTicTac {
	public Image panel;
	public Text text;
	public Button button;
}

[System.Serializable]
public class PlayerColor {
	public Color panelColor;
	public Color textColor;
}

public class GameControllerTicTac : MonoBehaviour {

	public Text[] buttonList;
	public GameObject gameOverPanel;
	public Text gameOverText;
	public GameObject restartButton;

	public PlayerTicTac playerX;
	public PlayerTicTac playerO;
	public PlayerColor activePlayerColor;
	public PlayerColor inactivePlayerColor;
	public GameObject startInfo;

	private string playerSide;
	private int moveCount;

	void Awake () {
		SetGameControllerReferenceOnButtons();
		gameOverPanel.SetActive(false);
		moveCount = 0;
		restartButton.SetActive(false);
	}

	void StartGame ()
	{
		SetBoardInteractable(true);
		SetPlayerButtons (false);
		startInfo.SetActive(false);
	}

	void SetGameControllerReferenceOnButtons () {

		for (int i = 0; i < buttonList.Length; i++) {
			buttonList[i].GetComponentInParent< GridSpace>().SetGameControllerReference(this);
		}

	}

	void SetPlayerButtons (bool toggle) {
		playerX.button.interactable = toggle;
		playerO.button.interactable = toggle;  
	}

	public string GetPlayerSide () {
		return playerSide;
	}

	void ChangeSides () {
		playerSide = (playerSide == "X") ? "O" : "X";
		if (playerSide == "X") {
			SetPlayerColors(playerX, playerO);
		} else {
			SetPlayerColors(playerO, playerX);
		}
	}

	public void EndTurn () {
		moveCount++;
		if (buttonList [0].text == playerSide && buttonList [1].text == playerSide && buttonList [2].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [3].text == playerSide && buttonList [4].text == playerSide && buttonList [5].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [6].text == playerSide && buttonList [7].text == playerSide && buttonList [8].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [0].text == playerSide && buttonList [3].text == playerSide && buttonList [6].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [1].text == playerSide && buttonList [4].text == playerSide && buttonList [7].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [2].text == playerSide && buttonList [5].text == playerSide && buttonList [8].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [2].text == playerSide && buttonList [4].text == playerSide && buttonList [6].text == playerSide) {
			GameOver (playerSide);
		}

		// Check that the game should not end as a draw
		else if (moveCount >= 9) {
			GameOver ("draw");
		} else {
			ChangeSides ();
		}
	}

	public void SetStartingSide (string startingSide)
	{
		playerSide = startingSide;
		if (playerSide == "X")
		{
			SetPlayerColors(playerX, playerO);
		} 
		else
		{
			SetPlayerColors(playerO, playerX);
		}
		StartGame();
	}

	void GameOver(string winningPlayer) {

		if (winningPlayer == "draw") {
			SetGameOverText("It's a Draw!");
			SetPlayerColorsInactive ();
		} else {
			SetGameOverText(winningPlayer + " Wins!");
		}

		SetBoardInteractable(false);
		restartButton.SetActive(true);
	}

	void SetGameOverText(string value) {
		gameOverPanel.SetActive(true);
		gameOverText.text = value;
	}

	public void RestartGame() {
		moveCount = 0;
		gameOverPanel.SetActive(false);

		for (int i = 0; i < buttonList.Length; i++) {
			buttonList [i].text = "";
		}
		restartButton.SetActive(false);
		SetPlayerButtons (true);
		SetPlayerColorsInactive ();
		startInfo.SetActive(true);
	}

	void SetPlayerColorsInactive ()
	{
		playerX.panel.color = inactivePlayerColor.panelColor;
		playerX.text.color = inactivePlayerColor.textColor;
		playerO.panel.color = inactivePlayerColor.panelColor;
		playerO.text.color = inactivePlayerColor.textColor;
	}

	void SetBoardInteractable (bool toggle) {
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList[i].GetComponentInParent<Button>().interactable = toggle;
		}
	}

	void SetPlayerColors (PlayerTicTac newPlayer, PlayerTicTac oldPlayer) {
		newPlayer.panel.color = activePlayerColor.panelColor;
		newPlayer.text.color = activePlayerColor.textColor;
		oldPlayer.panel.color = inactivePlayerColor.panelColor;
		oldPlayer.text.color = inactivePlayerColor.textColor;
	}

}
