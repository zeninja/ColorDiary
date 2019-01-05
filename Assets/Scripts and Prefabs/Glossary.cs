using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;


public class Glossary : MonoBehaviour
{
    public SpriteRenderer sprite;
    public TMP_InputField inputField;




    List<TMP_InputField> allInputs = new List<TMP_InputField>();

    List<TextFader> hideableText = new List<TextFader>();  // only hide user-generated text when showing instructions
    List<TMP_Text> userText = new List<TMP_Text>();
    List<string> savedStrings = new List<string>();


    List<string> instructionsText = new List<string>() {
        "Press and hold a line",
        "to change its",
        "contents",
        "Drag inside a block",
        "to change its color",
        "Double tap a block",
        "to select its color",
        "for the day",
        ""
    };

    public int entryCount = 9;

    void Init()
    {
        CreatePalette();
    }

    void Start()
    {
        Init();
    }

	void CreatePalette() {
        float blockWidth      = ScreenInfo.w;
        float blockHeight     = ScreenInfo.h / (float) entryCount;
        float blockOffset     = ScreenInfo.h / 2;
        float blockHeightHalf = blockHeight  / 2;

		for(int i = 0; i < entryCount; i++) {

            float y = (entryCount - i) * blockHeight;

            SpriteRenderer s = Instantiate(sprite).GetComponent<SpriteRenderer>();
            s.transform.localScale = new Vector2(blockWidth, blockHeight);
            s.transform.position   = new Vector3(0, y - blockOffset - blockHeightHalf, 0);
            s.transform.SetParent(transform);

            TMP_InputField newInput = Instantiate(inputField);
            newInput.transform.SetParent(transform);
            newInput.transform.localScale = Vector3.one;

            newInput.transform.SetParent(s.transform);
            newInput.transform.localPosition = Vector3.zero;

            hideableText.Add(newInput.GetComponentInChildren<TextFader>());


            TMP_Text placeholder = newInput.transform.Find("TextArea/Placeholder").GetComponent<TMP_Text>();
            TMP_Text text        = newInput.transform.Find("TextArea/Text")       .GetComponent<TMP_Text>();





            // newInput.GetComponent<InputFieldController>().Init();
		}
	}

    public IEnumerator ShowGlossary(bool val)
    {
        foreach (TextFader t in hideableText)
        {
            StartCoroutine(t.ShowText(val));
            yield return Extensions.Wait(.05f);
        }
    }

    public void SaveUserText(int index, string content)
    {
        userText[index].text = content;
        savedStrings[index] = content;

        GlobalSettings.GetInstance().UpdateText(savedStrings);
    }

    public void ShowInstructions(bool val)
    {
        for (int i = 0; i < userText.Count; i++)
        {
            allInputs[i].text = val ? "" : savedStrings[i];
        }
    }


    public Extensions.Property screenRange;

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
