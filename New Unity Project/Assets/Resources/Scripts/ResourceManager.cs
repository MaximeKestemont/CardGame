using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {


	public GameObject menuPanel;

	public Text player1TextName;
	public Text player1TextFood;
	public Text player2TextName;
	public Text player2TextFood;

	public DropZone[] dropZones;

	void Awake() {
		dropZones = GameObject.FindObjectsOfType<DropZone>();
	}

}
