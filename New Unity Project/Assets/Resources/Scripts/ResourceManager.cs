using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {


	public GameObject menuPanel;

	// Player 1
	public Text player1TextName;
	public Text player1TextFood;

	public List<DeploymentZone> player1DeployementZoneList = new List<DeploymentZone>(); // Need a list because the editor does not support dict...
	public Dictionary<DeploymentZone.ZonePosition, DeploymentZone> player1DeployementZone = new Dictionary<DeploymentZone.ZonePosition, DeploymentZone>();

	// Player 2
	public Text player2TextName;
	public Text player2TextFood;
	public List<DeploymentZone> player2DeployementZoneList = new List<DeploymentZone>();
	public Dictionary<DeploymentZone.ZonePosition, DeploymentZone> player2DeployementZone = new Dictionary<DeploymentZone.ZonePosition, DeploymentZone>();

	public DropZone[] dropZones;


	void Awake() {
		dropZones = GameObject.FindObjectsOfType<DropZone>();

		player1DeployementZone.Add(DeploymentZone.ZonePosition.LEFT, player1DeployementZoneList[0]);
		player1DeployementZone.Add(DeploymentZone.ZonePosition.MIDDLE, player1DeployementZoneList[1]);

	}

}
