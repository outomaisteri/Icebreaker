using UnityEngine;
using System;
using System.Collections;

public class LevelEditor : MonoBehaviour {
	
	#region Julkiset jäsenmuuttujat
	
	public GUISkin guiSkin = null;
	
	// Ruudun koko
	public Vector3 gridSize = Vector3.one;
	
	// Käyttöliittymän kuvakkeiden koko
	public int iconWidth = 64;
	
	#endregion
	
	#region Yksityiset jäsenmuuttujat
	
	private Rect blockSelectorRect;
	
	// Alue jonne näytöllä voidaan piirtää
	private Rect drawableArea = new Rect();
	
	private int selectedBlock;
	
	private BlockManager blockManager;
	
	private MousePaint mousePaint;
	
	#endregion
	
	#region Unity-metodit
	
	// Use this for initialization
	void Start () {
		
		int blockSelectorWidth = iconWidth + 10;
		int blockSelectorHeight = Screen.height - 10;
		
		blockSelectorRect = new Rect(
			5, 
			5, 
			blockSelectorWidth,
			blockSelectorHeight);
		
		drawableArea = new Rect(
			blockSelectorWidth + 15,
			0,
			Screen.width - blockSelectorWidth - 15,
			Screen.height);
			
		selectedBlock = -1;
		
		blockManager = (BlockManager)GetComponent(typeof(BlockManager));
		
		mousePaint = (MousePaint)GetComponentInChildren(typeof(MousePaint));
		
		mousePaint.SetGridSize(gridSize);
		
		mousePaint.SetDrawableArea(drawableArea);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		
		if (guiSkin != null) {
			GUI.skin = guiSkin;
		}
		
		// Kuutio-ikkuna
		BlockSelectorGUI();

	}
	
	private void BlockSelectorGUI() {
		
		// Tehdään taustalaatikko
		GUI.Box(blockSelectorRect, "Blocks");
		
		GUILayout.BeginArea(blockSelectorRect);
		
		GUILayout.BeginHorizontal();
		
		GUILayout.Space(5);
		
		GUILayout.BeginVertical();
		
		GUILayout.Space(25);
		
		// Näytetään käytössä olevat kuutiot
		// ruudukossa
		int newBlock = GUILayout.SelectionGrid(
			selectedBlock, 
			blockManager.GetIcons(), 
			1, 
			GUILayout.MaxWidth(iconWidth));
		
		// Valitaan uusi kuutio
		if (newBlock != selectedBlock) {
			selectedBlock = newBlock;
			mousePaint.SelectBlock(blockManager.GetBlock(selectedBlock));
		}
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();
		
		GUILayout.EndHorizontal();
		
		GUILayout.EndArea();
		
	}
	
	#endregion
	
	#region Apu-metodit
	
	#endregion
	
}
