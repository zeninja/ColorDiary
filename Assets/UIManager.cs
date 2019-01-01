using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Glossary glossary;
	public Text ui_glossaryButton;
	// public GameObject ui_resetTextButton;

	void Start() {
		RefreshUI();
	}

	public void SwitchGlossaryState() {
		GameManager.GetInstance().SwitchGlossaryState();
		RefreshUI();
	}

	void RefreshUI() {
		StartCoroutine(glossary.ShowGlossary(GameManager.InGlossary()));
		ui_glossaryButton.text = GameManager.InGlossary() ? "Hide Glossary" : "Show Glossary";
		// ui_resetTextButton.SetActive(GameManager.InGlossary());
	}
}
