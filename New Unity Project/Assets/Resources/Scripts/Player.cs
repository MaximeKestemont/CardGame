using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player {

	public string playerName;

	private int playerNumber;
	private Text playerText;
	private bool isActive;

	public Player(string name, int playerNumber, Text playerText) {
		this.playerName = name;
		this.playerNumber = playerNumber;
		this.playerText = playerText;

		playerText.text = playerName;
		isActive = false;
	}

	public void SetActive(bool flag) {
		isActive = flag;
	}
		
}
