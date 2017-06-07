using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Zone where a player can deploy cards
public class DeploymentZone : MonoBehaviour {

	public List<Card> cardList = new List<Card>();
	public int foodNumber = 0;
	public int battleValue = 0;

	public Text battleValueText;	// TODO replace with symbols

	public void Awake() {
		SetBattleValue(0);
	}


	public void SetBattleValue(int number) {
		battleValue = number;
		battleValueText.text = number.ToString();
	}
}
