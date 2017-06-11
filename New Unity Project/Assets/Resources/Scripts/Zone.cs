using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Zone : MonoBehaviour {

	public List<Card> cardList = new List<Card>();

	public Color highlightColor;
	protected Color normalColor;

	public enum ZoneType {HAND, BOARD, DEPLOYMENT_ZONE, GRAVEYARD};
	public ZoneType type;

	void Awake() {
		// TODO THIS DOES NOT WORK - not called by the child class -> why ?
		normalColor = this.GetComponent<Image>().color;
	}


	/*
	=====================
	Highlight
	=====================
	*/
	public void Highlight() {
		Debug.Log("Color : " + normalColor.ToString());
		this.GetComponent<Image>().color = this.highlightColor;
	}

	/*
	=====================
	StopHighlight
	=====================
	*/
	public void StopHighlight() {
		this.GetComponent<Image>().color = this.normalColor;
	}

	/*
	=====================
	RemoveCard
	=====================
	*/
	public void RemoveCard(Draggable d) {
		Card c = d.GetComponent<Card>();
		if (c == null) {
			Debug.LogError("Trying to remove a draggable not attached to a card");
		} else {
			cardList.Remove(c);
		}
	}

	/*
	=====================
	AddCard
	=====================
	*/
	public void AddCard(Draggable d) {
		Card c = d.GetComponent<Card>();
		if (c == null) {
			Debug.LogError("Trying to add a draggable not attached to a card");
		} else {
			cardList.Add(c);
		}
	}
}
