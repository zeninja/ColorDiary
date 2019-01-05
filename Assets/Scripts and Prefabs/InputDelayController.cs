using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InputDelayController : MonoBehaviour {

	TMP_InputField field;
	public float holdThreshold = .5f;
	float holdDuration = 0;

	ColorSelector colorSelector;
	public int fieldIndex;

	// Use this for initialization
	void Start () {
		field = GetComponent<TMP_InputField>();
		GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate { OnValueChanged(); });
		colorSelector = GetComponent<ColorSelector>();


	}

	// Update is called once per frame
	void Update () {
		if(fieldIndex >= 8) {
			field.interactable = false;
			return;
		}

		if(colorSelector.BeingHeld()) {
			holdDuration += Time.deltaTime;
			
			if (holdDuration > holdThreshold) {
				field.interactable = true;
			}
		} else {
			field.interactable = false;
		}
	}


	public void OnValueChanged() {
		GetComponentInParent<Glossary>().SaveUserText(fieldIndex, transform.Find("Text").GetComponent<TMP_Text>().text);
	}


}
