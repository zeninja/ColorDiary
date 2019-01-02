using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour  {

	private static GlobalSettings instance;
	public static GlobalSettings GetInstance() { return instance; }

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	public List<string> glossary;
	public List<Color> colors;

	void Start() {

		LoadInfo();
	}

	void LoadInfo() {
		GameData d = SaveSystem.LoadInfo();
		glossary   = d.glossary;
		colors     = d.colors;
	}

	public void UpdateText(List<string> s) {
		glossary = s;
		SaveSystem.SaveInfo(this);
	}

	public void UpdateColors(List<Color> c) {
		colors = c;
		SaveSystem.SaveInfo(this);
	}
}
