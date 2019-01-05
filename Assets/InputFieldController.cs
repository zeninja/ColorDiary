using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldController : MonoBehaviour {

	public int fieldIndex;
	
	public void Init() {
		RectTransform r = GetComponent<RectTransform>();

		r.localPosition = Vector2.zero;
		r.sizeDelta 	= transform.parent.localScale;
	}

	void Start() {
		if(fieldIndex >= 8) {
			GetComponent<TMP_InputField>().interactable = false;
		}
	}
}
