using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Glossary glossary;

	// public List<GameObject> ui_Buttons;

	void Start() {
		RefreshUIText();
	}

	public void SwitchGlossaryState() {
		GameManager.GetInstance().SwitchGlossaryState();
		StartCoroutine(glossary.ShowGlossary(GameManager.InGlossary()));

		RefreshUIText();
	}

	public void SwitchInstructionsState() {
		GameManager.GetInstance().SwitchInstructionsState();
		glossary.ShowInstructions(GameManager.InInstructions());
		// RefreshUIText();
	}

	public void SwitchCalendarState() {
		GameManager.GetInstance().SwitchCalendarState();
		StartCoroutine(Calendar.GetInstance().RefreshCalendar(GameManager.InCalendar()));
		// RefreshUIText();
	}

	// public void SwitchButtonState() {
	// 	GameManager.GetInstance().SwitchButtonState();
	// 	RevealUI();
	// 	RefreshUIText();
	// }

	void RefreshUIText() {
		// ui_glossaryButton.text 	   = GameManager.InGlossary() 	  ? "Hide Glossary"     : "Show Glossary";
		// ui_instructionsButton.text = GameManager.InInstructions() ? "Hide Instructions" : "Show Instructions";
		// ui_calendarButton.text	   = GameManager.InCalendar()     ? "Hide Calendar"     : "Show Calendar";
		// ui_showButtons.text        = GameManager.ShowButtons()    ? "Hide Buttons"		: "Show Buttons";
	}

	// public GameObject buttons;
	// public GameObject toggleButton;

	// void RevealUI() {
	// 	foreach(GameObject button in ui_Buttons) {
	// 		buttons.SetActive(GameManager.ShowButtons());
	// 	}
	// 	// toggleButton.GetComponent<Image>().enabled = GameManager.ShowButtons();
	// 	// toggleButton.SetActive(false);
	// }
}
