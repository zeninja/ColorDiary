using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockFrame : MonoBehaviour {

	public List<SpriteRenderer> frameParts;
	ColorBlock cb;

	// Use this for initialization
	void Start () {
		frameParts = GetComponentsInChildren<SpriteRenderer>().ToList();
		cb = transform.GetComponentInParent<ColorBlock>();
		
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		foreach(SpriteRenderer s in frameParts) {
			s.color = new Color(s.color.r, s.color.g, s.color.b, cb.GetAlpha());
		}
	}
}
