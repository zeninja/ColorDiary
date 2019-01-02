using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputController : MonoBehaviour {

	InputField field;

	// Use this for initialization
	void Start () {
		field = GetComponent<InputField>();
		GetComponent<InputField>().onValueChanged.AddListener(delegate { OnValueChanged(); });
	}	

	// Update is called once per frame
	void Update () {
		field.interactable = GameManager.InGlossary();
	}

	public int fieldIndex;

	public void OnValueChanged() {
		GetComponentInParent<Glossary>().SaveUserText(fieldIndex, transform.Find("Text").GetComponent<Text>().text);
	}


}
