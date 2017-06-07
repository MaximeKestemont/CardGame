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

	private bool isActive;

	public Player(string name, int playerNumber, Text playerText, Text foodText) {
		this.playerName = name;
		this.playerNumber = playerNumber;
		this.playerText = playerText;
		this.foodText = foodText;

		playerText.text = playerName;
		isActive = false;

		// Initialize food 
		SetFoodNumber(3);
	}

	public void SetActive(bool flag) {
		isActive = flag;
	}






	/* ---------------------------- Getter / Setter -------------------------- */

	public void SetFoodNumber(int number) {
		foodNumber = number;
		foodText.text = "Food : " + number;
	}

	public int GetFoodNumber() {
		return foodNumber;
	}
		
}
