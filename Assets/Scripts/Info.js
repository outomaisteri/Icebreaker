#pragma strict

var skin : GUISkin;

function OnGUI() {

	GUI.skin = skin;

	GUI.Label(Rect(((Screen.width / 2) - 200), 100, 400, 200),
			("You are a penguin trapped in an icy cavern. You must find your way out of each " +
			"room by stepping on all the 'tiles' of ice to open the door."));
			
	GUI.Label(Rect(((Screen.width / 2) - 200), 300, 400, 100),
			("Press Space to cheat and open the door"));
			
	if (GUI.Button (Rect(((Screen.width / 2) - 70), 450, 140, 30), "Close")) {
	
		Application.LoadLevel("menu");
	}
}