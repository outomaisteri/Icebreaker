using UnityEngine;
using System; // Jakojäännöksen käyttöön
using System.Collections;
using System.Collections.Generic; // Tarvitaan List-olion käyttöön

public class MousePaint : MonoBehaviour {

	#region Julkiset jäsenmuuttujat
	
	// Yhden ruudun koko ruudukossa
	public Vector3 gridSize = Vector3.one;
	
	// Valitun kuution tyyppi
	public GameObject currentBlockType;
	
	// Lista saatavilla olevista kuutioista
	public GameObject[] availableBlockTypes = new GameObject[0];
	
	// Tyhjä peliobjekti, jonne tallennetaan 
	// kenttään lisätyt peliobjektit
	public GameObject levelData;
	
	#endregion
	
	#region Yksityiset jäsenmuuttujat
	
	// Hiiren sijainti
	private Vector3 worldPos = Vector3.zero;
	
	// Sijainti ruudukkoon kohdistettuna
	private Vector3 gridPos = Vector3.zero;
	
	// Väliaikainen kuutio joka piirretään
	// kohtaan jonne hiiri osoittaa ruudukossa
	private GameObject cursor = null;
	
	// Viimeisin lisätty kuutio
	private GameObject lastBlock = null;
	
	// Lista jossa pidetään kirjaa kentässä
	// olevista peliobjekteista
	private List<GameObject> levelObjects = new List<GameObject>();
	
	#endregion
	
	
	/**
	 * Unity-metodit
	 */
	
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
		
		// Valitaan ensimmäinen kuutiot
		if (availableBlockTypes.Length > 0) {
			SetCurrentBlockType(availableBlockTypes[0]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		// Haetaan hiiren sijainti ruudulla
		Vector3 mousePos = new Vector3(
			Input.mousePosition.x, 
			Input.mousePosition.y,
			transform.position.y);
		
		// Lasketaan kursorin sijainti
		if (cursor != null) {
			
			// Sijainti pelimaailman suhteen
			worldPos = Camera.main.ScreenToWorldPoint(mousePos);
			
			gridPos = new Vector3(
				worldPos.x - (worldPos.x % gridSize.x),
				worldPos.y - (worldPos.y % gridSize.y),
				worldPos.z - (worldPos.z % gridSize.z));
			
			cursor.transform.position = gridPos;
			
			// Ruudun keskikohdan koordinaatit
			//float x = worldPos.x - (worldPos.x % gridSize.x) + gridSize.x * 0.5f;
			//float y = worldPos.y - (worldPos.y % gridSize.y) + gridSize.y * 0.5f;
			//float z = worldPos.z - (worldPos.z % gridSize.z) + gridSize.z * 0.5f;
		}
		
		// Lisätään kuutio kenttään kun painetaan hiiren vasenta painiketta
		if (Input.GetMouseButton(0)) {

			AddBlock(gridPos);
		}
		// Poistetaan kuutio kentästä kun painetaan hiiren oikeaa painiketta
		else if (Input.GetMouseButton(1)) {
			
			RemoveBlock(gridPos);			
		}
		
	}
	
	/**
	 * Apu-metodit
	 */
	
	// Update is called once per frame
	void SetCurrentBlockType (GameObject block) {
	
		currentBlockType = block;

		// Tuhotaan vanha kursori
		if (cursor != null) {
			
			GameObject.DestroyImmediate(cursor);
		}
		
		// Luodaan kursoriin kopio uudesta kuutiosta
		if (currentBlockType != null) {
			
			cursor = (GameObject)Instantiate(currentBlockType, gridPos, Quaternion.identity);
			cursor.name = cursor.name.Replace("(Clone)", "(Cursor)");
		}
	}
	
	private bool AddBlock(Vector3 pos) {
		
		GameObject oldBlock = GetBlockFrom(pos);
			
		if (oldBlock != null) {
			
			if (oldBlock.name != currentBlockType.name) {
				
				RemoveBlock(pos);
				
				GameObject newBlock = (GameObject)Instantiate(currentBlockType, pos, Quaternion.identity);
				newBlock.name = newBlock.name.Replace("(Clone)", "");
				
				levelObjects.Add(newBlock);
				
				return true;
			}
			else {
				
				return false;
			}
		}
		else {
	
			GameObject newBlock = (GameObject)Instantiate(currentBlockType, pos, Quaternion.identity);
			newBlock.name = newBlock.name.Replace("(Clone)", "");
			
			levelObjects.Add(newBlock);
			
			return true;
		}
		
	}
	
	private bool RemoveBlock(Vector3 pos) {
		
		GameObject block = GetBlockFrom(pos);
		
		if (block != null) {
			// Poistetaan kuutio listasta
			levelObjects.Remove(block);
			
			// Poistetaan kuutio skenestä
			GameObject.DestroyImmediate(block);
			
			return true;
		}
		
		return false;
		
	}
	
	private GameObject GetBlockFrom(Vector3 gridPos) {
		
		foreach (GameObject block in levelObjects) {
			
			if (block.transform.position.Equals(gridPos)) {
				
				return block;
			}
		}
		
		return null;
	}
}
