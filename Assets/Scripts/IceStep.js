#pragma strict

// Jäihin liitetty skripti, joka vaihtaa jäiden tekstuureja

// Jäiden tekstuurit
var SolidTex : Texture;
var CrackedTex : Texture;
var BrokenTex : Texture;

// Monetikko jäälle on astuttu
var timesStepped : int = 0;

// Alussa ehjä tekstuuri
function Start() {

	renderer.material.mainTexture = SolidTex;
}

// Kutsutaan kun pelaaja astuu jäälle
function OnTriggerEnter(col : Collider) {

	timesStepped++;
	
	// Ensimmäisellä murtuu
	if (timesStepped == 1) {
		renderer.material.mainTexture = CrackedTex;
		// Kutsu AudioPlayback-skriptiin
		gameObject.SendMessage("IceCrack");
		// Kutsu LevelClearCheck-skriptiin
		col.SendMessage("SteppedOnIce");
	}
	// Toisella särkyy
	else {
		renderer.material.mainTexture = BrokenTex;
		// Kutsu AudioPlayback-skriptiin
		gameObject.SendMessage("IceBreak");
		col.SendMessage("IceBreak");
	}
}

// Kutsutaan LevelClearCheck-skriptistä(RestartLevel)
function Reset() {

	renderer.material.mainTexture = SolidTex;
	timesStepped = 0;
}