#pragma strict

// Tarkistaa milloin kenttä on päästy läpi
// Aloittaa kentän alusta tarvittaessa

private var allIce : GameObject[];
private var objects : GameObject[];
private var totalIceCount : int;

private var steppedIceCount : int = 0;
private var isClear : boolean = false;

// Lasketaan montako jäätä kensässä on
function Start() {
	
	allIce = GameObject.FindGameObjectsWithTag("Ice");
	objects = GameObject.FindGameObjectsWithTag("Button"); 
	totalIceCount = allIce.Length;
}

function Update() {

	if (Input.GetKeyDown(KeyCode.Space)) {
	
		OpenDoor();
	}
}

// Kutsutaan IceStep-skriptistä(OnTriggerEnter),
// kun kullekin jääpalalle astuu ensimmäisen kerran
function SteppedOnIce() {

	steppedIceCount++;
	
	if (steppedIceCount == totalIceCount) {
		OpenDoor();
	}
}

// Avataan ovi
function OpenDoor() {

	gameObject.SendMessage("PlayDoorOpen");
	GameObject.Find("Door").renderer.enabled = false;
	GameObject.Find("Door").collider.enabled = false;
	//GameObject.Find("Door").animation.Play("open");
}

// Kutsutaan GridMove-skriptistä(Respawn)
// "Nollaa" kentän
function RestartLevel() {

	steppedIceCount = 0;
	
	GameObject.Find("Door").renderer.enabled = true;
	GameObject.Find("Door").collider.enabled = true;
	
	for (var ice : GameObject in allIce) {
		// Muutetaan jäät takaisin alkutilaan
		ice.SendMessage("Reset");
	}
	
	for (var obj : GameObject in objects) {
		// Muutetaan jäät takaisin alkutilaan
		obj.SendMessage("Reset");
	}
}