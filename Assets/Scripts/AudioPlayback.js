#pragma strict

// Käytetyt äänet
var iceCrackClip : AudioClip;
var iceBreakClip : AudioClip;

// Kutsutaan IceStep-skriptistä(OnTriggerEnter)
function IceCrack() {

	audio.pitch = Random.Range(1, 5);
	audio.PlayOneShot(iceCrackClip);
}

// Kutsutaan IceStep-skriptistä(OnTriggerEnter)
function IceBreak() {

	audio.pitch = 1;
	audio.PlayOneShot(iceBreakClip);
}