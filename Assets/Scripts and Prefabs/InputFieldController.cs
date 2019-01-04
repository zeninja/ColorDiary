using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFieldController : MonoBehaviour {

	SpriteRenderer s;
	Vector2 extents;
	RectTransform textArea;

	void Start() {
		s = GetComponentInChildren<SpriteRenderer>();
		// s.transform.parent = null;
		textArea = transform.GetComponentInChildren<UnityEngine.UI.RectMask2D>().GetComponent<RectTransform>();
	}

	public void Init(float width, float height, int index, int count) {

		float blockHeight = (float)height / (float)count;
		float screenHalf  = height/2f;
		float blockHalf   = blockHeight/2f;
		
		float p = (float)index  / (float)count;
		float y = blockHeight * p - screenHalf + blockHalf;

		transform.localPosition = new Vector2(0, y);

		// transform.position = pos;
		extents = new Vector2(width, height);  // pixel extents of each input block
		// Debug.Log(extents + "; " + Screen.width);

		// s.transform.localScale = extents;

	}

	void Update() 
	{
		UpdateComponents();

		// Debug.Log(Input.mousePosition);
	}

	void UpdateComponents() {
		// textArea.sizeDelta     = extents;
		s.transform.localScale = extents;


		// s.transform.localScale = Camera.main.ScreenToWorldPoint(extents);
		// s.transform.localScale = new Vector2(extents.x, 10);// Screen.height * .85f / 8f);
	}
}
