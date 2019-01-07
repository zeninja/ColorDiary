using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;


public class Glossary : MonoBehaviour
{
    public SpriteRenderer sprite;
    public TMP_InputField inputField;

    public GameObject textFrame;


    List<TMP_InputField> allInputFields = new List<TMP_InputField>();

    List<TextFader> textFaders = new List<TextFader>();
    List<TMP_Text> userText = new List<TMP_Text>();
    List<string> savedStrings = new List<string>();


    List<string> instructionsText = new List<string>() {
        "Tap a line",
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

        List<TMP_Text> placeholders = new List<TMP_Text>();

		for(int i = 0; i < entryCount; i++) {

            float y = (entryCount - i) * blockHeight;

            SpriteRenderer s = Instantiate(sprite).GetComponent<SpriteRenderer>();
            s.transform.localScale = new Vector2(blockWidth, blockHeight);
            s.transform.position   = new Vector3(0, y - blockOffset - blockHeightHalf, 0);
            s.transform.SetParent(transform);

            GameObject t = Instantiate(textFrame);
            t.transform.localScale = new Vector2(blockWidth, blockHeight * 1.75f);
            t.transform.position   = new Vector3(0, y - blockOffset - blockHeightHalf, 0);
            t.transform.SetParent(transform);

            TMP_InputField newInput = Instantiate(inputField);
            newInput.transform.SetParent(transform);
            newInput.transform.localScale = new Vector3 (1, 1, 1);
            //
            newInput.transform.SetParent(t.transform);
            newInput.transform.localPosition = Vector3.zero;


            TMP_Text placeholder = newInput.transform.Find("Text Area/Placeholder").GetComponent<TMP_Text>();
            TMP_Text text        = newInput.transform.Find("Text Area/Text")       .GetComponent<TMP_Text>();
            placeholders.Add(placeholder);

            newInput.GetComponent<InputFieldController>().Init(i, this);

            s.GetComponent<InputFieldSelector>().Init(i, newInput);

            //

            allInputFields.Add(newInput);
            userText.Add(text);
            savedStrings.Add("");

            textFaders.Add(text.gameObject.GetComponent<TextFader>());
            textFaders.Add(placeholder.gameObject.GetComponent<TextFader>());
		}

        SetText(placeholders);
	}
    
    void SetText(List<TMP_Text> p) {
        for(int i = 0; i < p.Count; i++) {
            p[i].text = instructionsText[i];
        }
    }

    public IEnumerator ShowGlossary(bool val)
    {
        foreach (TextFader t in textFaders)
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
        for (int i = 0; i < allInputFields.Count; i++)
        {
            // Setting the text of the input field to nothing ("") means that it will
            // fallback automatically to the "placeholder" (instructions) value
            // allInputFields[i].text = val ? "" : savedStrings[i];


            if(val) {
                allInputFields[i].text = "";
            } else {
                allInputFields[i].text = savedStrings[i];
            }
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
		foreach(TMP_InputField t in allInputFields) {
			Gizmos.DrawWireSphere(t.transform.position, .05f);
		}
	}
}
