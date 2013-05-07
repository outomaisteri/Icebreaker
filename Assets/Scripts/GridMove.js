#pragma strict

// Liikkumisen muuttujat
private var walkSpeed : float = 2.0;
private var gridSize : int = 1;
private enum Orientation {Horizontal}
private var gridOrientation = Orientation.Horizontal;
private var input = Vector2.zero;
private var startPosition : Vector3;
private var endPosition : Vector3;
private var controllable : boolean = true;

// Respawn-muuttujat
private var spawnPosition : Vector3;
private var spawnRotation : Quaternion;

// Otetaan aloituspiste muistiin
// Aloitetaan liikkuminen
function Start () {
	spawnPosition = transform.position;
	spawnRotation = transform.rotation;
	Movement();
}

function Update() {

	
}

// Hoitaa hahmon liikkeen
function Movement() {

	var myTransform : Transform = transform;

	var t : float; // "Liikenopeus"
	var tx : float; // "Korjausnopeus"
	
	// Estetään, ettei pelaaja liiku kun kontrolli palautuu
	input = Vector2.zero;

	while (controllable) {
		// Kun ei liikuta...
		while (input == Vector2.zero) {
			transform.FindChild("pingu").animation.CrossFade("Idle", 0.1);
			GetAxes();
			tx = 0.0; // Lopettaa liikkeen?
			yield; // Jos ei yieldiä, loputon loop
		}
		
		// Suunta joko vaaka tai pysty, GetAxis määrittää inputit
		transform.forward = Vector3.Normalize(new Vector3(input.x, 0, input.y));
		
		// Otetaan oma nyk.sijainti
		startPosition = myTransform.position;
		
		if (!Physics.Raycast(transform.position, transform.forward, 1)) {
			// "Määränpääksi" gridin pituus haluttuun suuntaan
			endPosition = Vector3(Mathf.Round(myTransform.position.x), Mathf.Round(myTransform.position.y), Mathf.Round(myTransform.position.z)) +
					Vector3(System.Math.Sign(input.x) * gridSize, 0, System.Math.Sign(input.y) * gridSize);
			
		}
		else {
			endPosition = startPosition;
			gameObject.SendMessage("PlayPlayerCol");
		}
		
		t = tx;
		
		while (t < 1.0) {
			transform.FindChild("pingu").animation.CrossFade("Walk", 0.1);
			t += Time.deltaTime * (walkSpeed/gridSize); // pastea takaisin jos tarvii
			myTransform.position = Vector3.Lerp(startPosition, endPosition, t);
			yield;
		}
		
		if (!controllable) {
			transform.FindChild("pingu").animation.Stop();
		}
		
		tx = t - 1.0;	// Used to prevent slight visual hiccups on "grid lines" due to Time.deltaTime variance
		GetAxes();
	}
}

// Vaihtaa hahmon suuntaa
function GetAxes () {
	// Laitetaan input:iin Unityn inputin arvot
	input = Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

	// Vain toinen akseli voi olla "aktiivinen"
	if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
		input.y = 0.0;
	else
		input.x = 0.0;
}


// --------------------
// --------------------



// Estää kävelemästä kivien ja oven läpi
function OnCollisionEnter(col : Collision) {
	
	if (col.gameObject.name == "Rock" || col.gameObject.name == "Door") {
		endPosition = startPosition;
	}
}

function OnTriggerEnter(col : Collider) {

	// Kun päästään maaliin, kenttä loppuu
	if (col.gameObject.name == "Exit") {
		controllable = false;
		gameObject.SendMessage("PlayLevelClear");
		while (transform.FindChild("pingu").animation.isPlaying) {
			yield;
		}
		transform.FindChild("pingu").animation.Play("Victory");
		transform.FindChild("pingu").animation.wrapMode = WrapMode.Loop;
		yield WaitForSeconds(2);
		gameObject.SendMessage("EndLevel");
	}
	
	if (col.gameObject.name == "Broken Ice") {
		IceBreak();
	}
}

// Kun pelaaja tippuu jään läpi, estetään liikkuminen
function IceBreak() {

	controllable = false;
	yield WaitForSeconds(1);
	transform.FindChild("pingu").animation.Play("Death");
	transform.FindChild("pingu").animation.wrapMode = WrapMode.Once;
	yield WaitForSeconds(3);
	Respawn();
}

// Siirretään hahmo alkupisteeseen
function Respawn() {

	gameObject.SendMessage("RestartLevel");
	transform.position = spawnPosition;
	transform.rotation = spawnRotation;
	transform.FindChild("pingu").animation.Play("Idle");
	transform.FindChild("pingu").animation.wrapMode = WrapMode.PingPong;
	controllable = true;
	Movement();
}