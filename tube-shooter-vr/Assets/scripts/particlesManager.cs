using UnityEngine;
using System.Collections;

public class particlesManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!particleSystem.IsAlive()) {
			if(transform.parent.name == "player") {
				transform.parent.renderer.enabled = true;
				transform.parent.GetChild(0).renderer.enabled = true;
				transform.parent.gameObject.collider.enabled = true;
			}
			Destroy(gameObject);

		}
	}
}
