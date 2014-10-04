using UnityEngine;
using System.Collections;

public class states : MonoBehaviour {
	static public int state;
	private string text;
	static public int GAME_STATE = 1;
	static public int MENU_STATE = 0;
	static public bool godMod = false;
	// Use this for initialization
	void Start () {
		state = MENU_STATE;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton9)) {
			state = state==MENU_STATE ? MENU_STATE:GAME_STATE;
		}
	}
	void OnGUI() {
		if(GUI.Button(new Rect(0, 0, 100, 50), "Play")) {
			state = state==GAME_STATE ? MENU_STATE:GAME_STATE;
		}
		if(GUI.Button(new Rect(0, 50, 100, 50), "God Mod")) {
			godMod = true;
		}
	}

}
