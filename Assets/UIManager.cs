using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Glossary glossary;
	public Text ui_glossaryButton;
	public Text ui_instructionsButton;
	public Text ui_calendarButton;

	// public GameObject ui_instructionsButton;

	void Start() {
		RefreshUIText();
	}

	public void SwitchGlossaryState() {
		GameManager.GetInstance().SwitchGlossaryState();
		StartCoroutine(glossary.ShowGlossary(GameManager.InGlossary()));

		// if (!GameManager.InGlossary()) {

		// }

		RefreshUIText();
	}

	public void SwitchInstructionsState() {
		GameManager.GetInstance().SwitchInstructionsState();
		glossary.ShowInstructions(GameManager.InInstructions());
		RefreshUIText();
	}

	public void SwitchCalendarState() {
		GameManager.GetInstance().SwitchCalendarState();
		StartCoroutine(Calendar.GetInstance().RefreshCalendar(GameManager.InCalendar()));
		RefreshUIText();
	}

	void RefreshUIText() {
		ui_glossaryButton.text 	   = GameManager.InGlossary() 	  ? "Hide Glossary"     : "Show Glossary";
		ui_instructionsButton.text = GameManager.InInstructions() ? "Hide Instructions" : "Show Instructions";
		ui_calendarButton.text	   = GameManager.InCalendar()     ? "Hide Calendar"     : "Show Calendar";
	}

	void RevealUI() {
		
	}
}
