using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGlossary : MonoBehaviour {

	GridLayoutGroup grp;
	public TMP_InputField input;
	List<TMP_InputField> allInputs = new List<TMP_InputField>();


	void Start() {
		grp = GetComponent<GridLayoutGroup>();
		CreatePalette();
	}

	public int entryCount = 8;

	public Extensions.Property screenRange;

	float multiplier = 1;

	// void CreatePalette() {
	// 	// width and height are in viewport space where 1 = top (right) and 0 = bottom (left)
	// 	// float screenExpanse = screenRange.end - screenRange.start;
	// 	// float blockWidth    = 1f;
	// 	// float blockHeight   = screenExpanse / (float)entryCount;


	// 	// Vector2 b = Camera.main.ViewportToScreenPoint( new Vector2(blockWidth, blockHeight));

	// 	for(int i = 0; i < entryCount; i++) {
	// 		TMP_InputField newInput = Instantiate(input);
	// 		newInput.transform.SetParent(transform);
	// 		newInput.transform.localScale = Vector3.one;

	// 		InputFieldController ifc = newInput.GetComponent<InputFieldController>();
			
	// 		ifc.Init(b.x, b.y);

	// 		allInputs.Add(newInput);
	// 	}

	// 	Vector2 blockSize = grp.cellSize;
	// 	blockSize.y = b.y;
		
	// 	grp.cellSize = blockSize;

	// 	// Debug.Log(Screen.height * screenExpanse)

	// 	// Debug.Log(blockSize.y);
	// }

	void CreatePalette() {
		float screenExpanse = screenRange.end - screenRange.start;
		float blockWidth = 1;
		float blockHeight = screenExpanse / (float)entryCount;

		Vector2 b = Camera.main.ViewportToScreenPoint( new Vector2(blockWidth, blockHeight));

		for(int i = 0; i < entryCount; i++) {
			TMP_InputField newInput = Instantiate(input);
			newInput.transform.SetParent(transform);
			newInput.transform.localScale = Vector3.one;

			InputFieldController ifc = newInput.GetComponent<InputFieldController>();
			
			// ifc.Init(b.x, b.y);

			allInputs.Add(newInput);
		}

		Vector2 blockSize = grp.cellSize;
		blockSize.y = b.y;
		
		grp.cellSize = blockSize;
	}

	void OnDrawGizmos() {
		// 0, 0 is bottom left, 1, 1 is top right

		Gizmos.color = Color.white;

		for(int i = 0; i <= entryCount; i++) {
			float p = (float)(entryCount - i) / (float)entryCount;

			float y = Extensions.GetLinearRange(screenRange, p);
			// Debug.Log(y);
		
			Camera.main.ViewportToScreenPoint( new Vector3(0, y, 0));
			Gizmos.DrawWireSphere(Camera.main.ViewportToWorldPoint(new Vector3(0, y, 0)), .5f);
		}

		Gizmos.color = Color.green;
		foreach(TMP_InputField t in allInputs) {
			Gizmos.DrawWireSphere(t.transform.position, .5f);
		}
	}
}
