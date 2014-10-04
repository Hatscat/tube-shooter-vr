using UnityEngine;
using System.Collections;

public class simpleCamMove : MonoBehaviour {
	public float Xspeed;
	public float Yspeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(-Input.GetAxis("rightStickY")*Yspeed,Input.GetAxis("rightStickX")*Xspeed,0);
	}
}
