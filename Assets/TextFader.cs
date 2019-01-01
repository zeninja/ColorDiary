using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour {

	Color start;
	Color current;

	void Awake() {
		start = GetComponent<Text>().color;
	}

	public IEnumerator ShowText(bool val) {

		float t = 0;
		float d = .25f;
		float a = 0;

        while (t < d)
        {
			t += Time.fixedDeltaTime;
			float p = Mathf.Clamp01( t / d );

            if (val)
            {
				a = start.a * EZEasings.SmoothStop3(p);
            }
            else
            {
				a = start.a - start.a * EZEasings.SmoothStart3(p);
            }
			
			current = new Color(start.r, start.g, start.b, a);
			GetComponent<Text>().color = current;

			yield return new WaitForFixedUpdate();
        }
	}
}
