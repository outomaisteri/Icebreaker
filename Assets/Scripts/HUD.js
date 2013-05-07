#pragma strict

var playerColClip : AudioClip;
var levelCompClip : AudioClip;
var doorOpenClip : AudioClip;
var skin : GUISkin;

private var levelComplete : boolean = false;

private var windowRect : Rect = Rect(
		((Screen.width / 2) - 250), ((Screen.height / 2) - 200), 500, 400);

function OnGUI() {

	GUI.skin = skin;
	
	// Loppuikkuna
	if (levelComplete) {
		windowRect = GUI.Window(0, windowRect, EndMessage, "Level Complete!");
	}
}

function EndLevel() {

	levelComplete = true;
}

function EndMessage() {

	GUI.Label(Rect(((windowRect.width / 2) - 150), 100, 300, 50),
			("Asd"));
			
	if (GUI.Button (Rect(((windowRect.width / 2) - 70), 300, 140, 30), "Next Level")) {
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}

function PlayPlayerCol() {

	audio.PlayOneShot(playerColClip);
}

function PlayDoorOpen() {

	audio.PlayOneShot(doorOpenClip);
}

function PlayLevelClear() {

	audio.PlayOneShot(levelCompClip);
}