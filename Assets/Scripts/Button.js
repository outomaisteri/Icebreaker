#pragma strict

var door : Transform;

function OnTriggerEnter(col : Collider) {

	door.renderer.enabled = false;
	door.collider.enabled = false;
}

function Reset() {

	door.renderer.enabled = true;
	door.collider.enabled = true;
}