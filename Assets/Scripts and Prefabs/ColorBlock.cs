using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlock : MonoBehaviour {

	SpriteRenderer s;

	void Start() {
		s = GetComponent<SpriteRenderer>();
		s.color = new Color(1, 1, 1, 0);
		frame = transform.Find("Frame");
	}

	public void SetColor(Color n) {
		color   = n;
		s.color = color;
	}

	public Color color;

	public bool selected;

	Transform frame;

	public void SetSelected(bool val) {
		selected = val;
		frame.gameObject.SetActive(val);
	}


	public IEnumerator RevealBlock(bool val) {
		float t = 0;
		float d = .15f;

		float a = val ? 1f : 0f;

		while (t < d) {
			t += Time.fixedDeltaTime;
			float p = Mathf.Clamp01(t / d);

			if(val) {
				alpha   = a * EZEasings.SmoothStop3(p);
				s.color = new Color(color.r, color.g, color.b, alpha);
			} else {
				alpha   = a - a * EZEasings.SmoothStart3(p);
				s.color = new Color(color.r, color.g, color.b, alpha);
			}

			yield return new WaitForFixedUpdate();
		}

	}

	float alpha = 1;

	public float GetAlpha() {
		return alpha;
	}
}
