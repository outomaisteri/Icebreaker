// Piilotetaan aiheettomat varoitukset editorin debug logista
#pragma warning disable 0414

using UnityEngine;
using System;
using System.Collections;

public class BlockManager : MonoBehaviour {
	
	#region Julkiset jäsenmuuttujat
	
	// Lista käytettävissä oleville kuutioille
	public GameObject[] availableBlocks = new GameObject[0];
	
	#endregion
	
	#region Yksityiset jäsenmuuttujat
	
	// Lista kuutoiden kuvakkeille
	private Texture[] blockIcons = new Texture[0];
	
	#endregion
	
	#region Unity-metodit
	
	// Use this for initialization
	void Start() {
	
		// Alustetaan lista kuvakkeille
		blockIcons = new Texture[availableBlocks.Length];
		
		// Haetaan kuutioiden kuvakkeet
		for (int i = 0; i < availableBlocks.Length; i++) {
			blockIcons[i] = 
				(Texture)Resources.Load("Icons/" + availableBlocks[i].name, typeof(Texture));
		}
	}
	
	#endregion
	
	#region Apu-metodit
	
	// Palauttaa kuution annetusta indeksistä
	public GameObject GetBlock(int index) {
		
		if (index >= 0 && index < availableBlocks.Length) {
			
			return availableBlocks[index];
		}
		else {
			
			return null;
		}
	}
	
	// Palauttaa kaikki kuvakkeet
	public GameObject[] GetBlocks() {
		
		return availableBlocks;
	}

	// Palauttaa kuvakkeen annetusta indeksistä
	public Texture GetIcon(int index) {
		
		if (index >= 0 && index < blockIcons.Length) {
			
			return blockIcons[index];
		}
		else {
			
			return null;
		}
	}

	// Palauttaa kaikki kuvakkeet
	public Texture[] GetIcons() {
		
		return blockIcons;
	}

	#endregion
	
}
