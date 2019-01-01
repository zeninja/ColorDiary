using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlasher : MonoBehaviour {

	SpriteRenderer s;

	// Use this for initialization
	void Start () {
		s = GetComponent<SpriteRenderer>();
		s.transform.localScale = new Vector2(ScreenInfo.w, ScreenInfo.h);
		s.color = new Color(1, 1, 1, 0);

		EventManager.StartListening("DoubleTap", HandleDoubleTap);
	}

	public void TriggerFlash() {
		StartCoroutine("Flash");
	}

	public Color flashColor;
	public float flashDuration = .3f;

	public bool flashOnDoubleTap;

	void HandleDoubleTap() {
		if(flashOnDoubleTap) {
			StartCoroutine("Flash");
		}
	}

	IEnumerator Flash() {
		float d = flashDuration;
		float t = 0;

		while (t < d) {
			t += Time.fixedDeltaTime;
			float p = t / d;

			float alpha = (1 - EZEasings.SmoothStop3(p)) * flashColor.a;
			Color adjustedColor = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);

			s.color = adjustedColor;
			yield return new WaitForFixedUpdate();
		}
	}
}
