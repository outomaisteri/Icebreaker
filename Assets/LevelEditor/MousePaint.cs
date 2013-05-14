using UnityEngine;
using System; // Jakojäännöksen käyttöön
using System.Collections;
using System.Collections.Generic; // Tarvitaan List-olion käyttöön

public class MousePaint : MonoBehaviour {

	#region Julkiset jäsenmuuttujat
		
	// Tyhjä peliobjekti, jonne tallennetaan 
	// kenttään lisätyt peliobjektit
	public GameObject levelData;
	
	#endregion
	
	#region Yksityiset jäsenmuuttujat
	
	// Väliaikainen kuutio joka piirretään
	// kohtaan jonne hiiri osoittaa ruudukossa
	private GameObject cursor = null;
	
	// Valitun kuution tyyppi
	private GameObject selectedBlock;
	
	// Yhden ruudun koko ruudukossa
	private Vector3 gridSize = Vector3.one;
	
	// Sijainti ruudukkoon kohdistettuna
	private Vector3 gridPos = Vector3.zero;

	// Alue jonne näytöllä voidaan piirtää
	private Rect drawableArea = new Rect(0, 0, Screen.width, Screen.height);
	
	// Lista jossa pidetään kirjaa kentässä
	// olevista peliobjekteista
	private List<GameObject> levelObjects = new List<GameObject>();
	
	#endregion
	
	#region Unity-metodit
	
	// VÄLIAIKAINEN RATKAISU
	// Use this for initialization
	void Start () {
		
		// Haetaan kentän vanhat peliobjektit
		foreach (Component component in levelData.GetComponents(typeof(GameObject))) {
			// TODO: Mietitään millä tunnistetaan tallennetut tiedot/kuutiot,
			// 		 voidaan käyttää esim. peliobjektin nimeä tai tagia.
			if (component.GetType() == typeof(GameObject)) {
				GameObject block = component.gameObject;
				
				switch(block.tag) {
				default:
					levelObjects.Add(block);
					break;
				}
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
		// Tarkistetaan että hiiri on piirrettävän 
		// alueen sisällä
		if (!drawableArea.Contains(Input.mousePosition)) {
			return;
		}
		
		// Haetaan hiiren sijainti ruudulla
		Vector3 mousePos = new Vector3(
			Input.mousePosition.x, 
			Input.mousePosition.y,
			transform.position.y);
		
		// Lasketaan kursorin sijainti
		if (cursor != null) {
			
			// Sijainti pelimaailman suhteen
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
			
			gridPos = new Vector3(
				Mathf.Floor(worldPos.x - (worldPos.x % gridSize.x)),
				Mathf.Floor(worldPos.y - (worldPos.y % gridSize.y)),
				Mathf.Floor(worldPos.z - (worldPos.z % gridSize.z)));
			
			// Nostetaan kursoria hieman kuutoiden yläpuolelle
			// että se ei jää kentällä olevien kuutioiden alle
			cursor.transform.position = gridPos + new Vector3(0, 0.01f, 0);
		}
		
		
		// Lisätään kuutio kenttään hiiren vasemmalla painikkeella
		if (Input.GetMouseButton(0)) {
			
			GameObject oldBlock = GetBlockFrom(gridPos);
			
			if (oldBlock != null && selectedBlock != null) {
				if (oldBlock.name != selectedBlock.name) {			
					RemoveBlock(gridPos);			
					AddBlock(gridPos, selectedBlock);
				}
			}
			else {
				AddBlock(gridPos, selectedBlock);
			}
		}
		// Piilotetaan kursori kun aletaan pyyhkimään kuutioita
		else if (Input.GetMouseButtonDown(1)) {
			
			if (cursor != null) {
				cursor.renderer.enabled = false;
			}
		}
		// Näytetään kursori kun kuutioiden poistaminen lopetetaan
		else if (Input.GetMouseButtonUp(1)) {
			
			if (cursor != null) {
				cursor.renderer.enabled = true;
			}
		}
		// Poistetaan kuutio kentästä hiiren oikealla painikkeella
		else if (Input.GetMouseButton(1)) {
			
			RemoveBlock(gridPos);			
		}
		
	}
	
	#endregion
	
	#region Apu-metodit
	
	public void SetGridSize(Vector3 size) {
		
		gridSize = size;
	}
	
	public void SetDrawableArea(Rect area) {
		
		drawableArea = area;
	}
	
	public void SelectBlock (GameObject block) {
	
		selectedBlock = block;

		// Tuhotaan vanha kursori
		if (cursor != null) {
			
			GameObject.DestroyImmediate(cursor);
		}
		
		// Luodaan kursoriin kopio uudesta kuutiosta
		if (selectedBlock != null) {
			
			cursor = (GameObject)Instantiate(selectedBlock, gridPos, Quaternion.identity);
			cursor.name = cursor.name.Replace("(Clone)", "(Cursor)");
			
			if (cursor.collider != null) {
				cursor.collider.enabled = false;
			}
		}
	}
	
	private void AddBlock(Vector3 pos, GameObject block) {
		
		if (block != null) {
			// Luodaan halusta kuutiotyypistä kopio
			GameObject newBlock = (GameObject)Instantiate(block, pos, Quaternion.identity);
			
			// Muokataan kopion nimeä ja asetetaan se isäntä objektiin
			newBlock.name = newBlock.name.Replace("(Clone)", "");
			newBlock.transform.parent = levelData.transform;
			
			// Lisätään kuutio listaan
			levelObjects.Add(newBlock);	
		}
	}
	
	private void RemoveBlock(Vector3 pos) {
		
		// Etsitään kuutio
		GameObject block = GetBlockFrom(pos);
		
		if (block != null) {
			// Poistetaan kuutio listasta
			levelObjects.Remove(block);
			
			// Poistetaan kuutio skenestä
			GameObject.DestroyImmediate(block);
		}
	}
	
	private GameObject GetBlockFrom(Vector3 gridPos) {
		
		foreach (GameObject block in levelObjects) {
			
			if (block.transform.position.Equals(gridPos)) {
				
				return block;
			}
		}
		
		return null;
	}
	
	#endregion
}
