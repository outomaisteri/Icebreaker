using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	#region Julkiset jäsenmuuttujat
		
	public float moveSpeed = 1;
	public float zoomSpeed = 1;
	public float minZoom = 2;
	public float maxZoom = 20;
	
	#endregion
	
	#region Yksityiset jäsenmuuttujat
	
	private Camera cam;
	
	#endregion
	
	#region Unity-metodit
	
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.DownArrow)) {
			transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
		}
		
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
		}
		
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
		}
		
		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.Translate(0, moveSpeed * Time.deltaTime, 0);
		}
		
		if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus)) {
			if (camera.orthographicSize > minZoom) {
				transform.Translate(0, 0, zoomSpeed * Time.deltaTime);
				camera.orthographicSize -= zoomSpeed * Time.deltaTime;
			}
		}
		else if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus)) {
			if (camera.orthographicSize < maxZoom) {
				transform.Translate(0, 0, -zoomSpeed * Time.deltaTime);	
				camera.orthographicSize += zoomSpeed * Time.deltaTime;		
			}
		}
	}
	
	#endregion
	
}
