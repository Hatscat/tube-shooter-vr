﻿using UnityEngine;
using System.Collections;

public class menu_bt : MonoBehaviour {

	public int lvl2load;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1")) {
			Application.LoadLevel(1);
		}
	}

	void OnMouseDown () {
		Application.LoadLevel(lvl2load);
	}
}
