using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

	public List<string> glossary;
	public List<Color> colors;

	public GameData(GlobalSettings info) {
		glossary = info.glossary;
		colors   = info.colors;
	}
}
