using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldSelector : MonoBehaviour {

	// check for input every frame (via on mouse down like in the color selector)
	
	int fieldIndex;
	TMP_InputField field;

	ColorSelector colorSelector;

	public  float holdThreshold = .3f;
	private float holdDuration;

	public void Init(int i, TMP_InputField f) {
		fieldIndex = i;
		field = f;
	}

	void Start() {
		colorSelector = GetComponent<ColorSelector>();
	}

	void Update() {
		if (fieldIndex >= 8) {
			field.interactable = false;
			colorSelector.canAddColorToCalendar = false;
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
}
