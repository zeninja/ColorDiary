using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;


public class Glossary : MonoBehaviour
{

    public TMP_InputField input;
    List<TMP_InputField> allInputs = new List<TMP_InputField>();

    List<TextFader> hideableText = new List<TextFader>(); // We only want to hide text



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

        float screenExpanse = screenRange.end - screenRange.start;
        float x = ScreenInfo.x;
        float y = ScreenInfo.y * screenExpanse;


		for(int i = 0; i < entryCount; i++) {
			TMP_InputField newInput = Instantiate(input);
            newInput.transform.SetParent(transform);
			newInput.transform.localScale = Vector3.one;

            newInput.GetComponent<InputDelayController>().fieldIndex = i;    // TODO: roll this in to InputFieldController

			InputFieldController ifc = newInput.GetComponent<InputFieldController>();
            ifc.Init(x, y, i, entryCount);


            userText.Add(newInput.transform.Find("TextArea/Text").GetComponent<TMP_Text>());
            userText[i].text = instructionsText[i];
            savedText.Add("");



			allInputs.Add(newInput);
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

    List<TMP_Text> userText = new List<TMP_Text>();
    List<string> savedText = new List<string>();

    public void SaveUserText(int index, string content)
    {
        userText[index].text = content;
        savedText[index] = content;

        GlobalSettings.GetInstance().UpdateText(savedText);
    }

    public void ShowInstructions(bool val)
    {
        for (int i = 0; i < userText.Count; i++)
        {
            allInputs[i].text = val ? "" : savedText[i];
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
