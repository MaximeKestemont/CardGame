using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour {

	private string _gameVersion = "0.1";
	public GameObject sorcierPrefab;
 
	void Start () {
		PhotonNetwork.ConnectUsingSettings (_gameVersion);
	}

	void Update () {
		//Debug.Log("Status : " + PhotonNetwork.connectionStateDetailed.ToString());

	}

	void OnJoinedLobby() {
		Debug.Log ("Trying to connect to a random room");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed() {
		Debug.Log ("Cant join random room");
		PhotonNetwork.CreateRoom (null);
	}

	void OnJoinedRoom() {
		Debug.Log ("Room joined !");
		PhotonNetwork.Instantiate ("Prefabs/" + sorcierPrefab.name, sorcierPrefab.transform.position, Quaternion.identity, 0);
	}
}
