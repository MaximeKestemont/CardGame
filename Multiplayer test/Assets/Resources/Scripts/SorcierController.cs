﻿using UnityEngine; 
using System.Collections; 


public class SorcierController : MonoBehaviour { 
	float speed = 1000f; 
	PhotonView view;

	// Use this for initialization 
	void Start () { 
		view = GetComponent<PhotonView> ();
	} 

	// Update is called once per frame 
	void FixedUpdate () { 
		float h = Input.GetAxis ("Horizontal"); 
		float v = Input.GetAxis ("Vertical"); 
		Vector3 movement = new Vector3 (h, 0.0f, v); 
		if (h != 0f || v != 0f) {
			if (view.isMine)
				GetComponent<Rigidbody>().velocity = movement *speed * Time.deltaTime; 
		}
	} 

	void Update () { } 

}﻿