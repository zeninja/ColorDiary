using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlock : MonoBehaviour {

	public void SetColor(Color n) {
		GetComponent<SpriteRenderer>().color = n;
	}

	void Start() {
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
		frame = transform.Find("Frame");
	}

	public bool selected;

	Transform frame;

	public void SetSelected(bool val) {
		selected = val;
		frame.gameObject.SetActive(val);
	}
}
