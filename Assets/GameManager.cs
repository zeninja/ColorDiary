﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	#region old state stuff (not bad to use!)
	// public enum GameState { colorSelection, glossary };
	// private static GameState state = GameState.colorSelection;
	// public static GameState GetCurrentState() { return state; }
	// public static bool InGlossary() { return state == GameState.glossary; }


	// int stateIndex = 0;

	// public void IncrementState() {
	// 	stateIndex = (stateIndex + 1) % System.Enum.GetNames(typeof(GameState)).Length;
	// 	state 	   = (GameState)stateIndex;

	// }
	#endregion

	public static bool InGlossary() { return showGlossary; }
	private static bool showGlossary = true;

	private static GameManager instance;
	public static GameManager GetInstance() {
		return instance;
	}

	void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			if(this != instance) {
				Destroy(gameObject);
			}
		}
	}

	public void SwitchGlossaryState() {
		showGlossary = !showGlossary;
	}
}