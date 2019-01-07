using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldController : MonoBehaviour {

	public int fieldIndex;

	TMP_InputField inputField;
	
	Glossary glossary;
	public void Init(int i, Glossary g) {
		RectTransform r = GetComponent<RectTransform>();

		r.localPosition = Vector2.zero;
		r.sizeDelta 	= transform.parent.localScale;
		glossary   = g;
		fieldIndex = i;

		// if(inputField.enabled) {
		// 	inputField.enabled = false;
		// 	inputField.enabled = true;
		// }
	}

	void Awake() {
		inputField = GetComponent<TMP_InputField>();
		tmpText	   = transform.Find("Text Area/Text").GetComponent<TMP_Text>();

		if(fieldIndex >= 8) {
			inputField.interactable = false;
			inputField.enabled = false;
			
		}

		GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate { OnValueChanged(); });

	}

	TMP_Text tmpText;

	public void OnValueChanged() {
		glossary.SaveUserText(fieldIndex, tmpText.text);
	}
}
