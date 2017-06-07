using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {


	public GameObject menuPanel;

	public Text player1Text;
	public Text player2Text;

	public DropZone[] dropZones;

	void Awake() {
		dropZones = GameObject.FindObjectsOfType<DropZone>();
	}

}
