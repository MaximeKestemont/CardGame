using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

	Text statusText;
	Text masterText;

	// Use this for initialization
	void Start () {
		statusText = GameObject.Find ("statusText").GetComponent<Text> ();
		masterText = GameObject.Find ("masterText").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		statusText.text = "Status : " + PhotonNetwork.connectionStateDetailed.ToString ();
		masterText.text = "isMaster : " + PhotonNetwork.isMasterClient;
	}
}
