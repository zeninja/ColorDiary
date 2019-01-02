using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class Glossary : MonoBehaviour
{

    public UnityEngine.UI.InputField input;
    List<UnityEngine.UI.InputField> allInputs = new List<UnityEngine.UI.InputField>();

    List<TextFader> hideableText = new List<TextFader>(); // We only want to hide text

    List<string> instructionsText = new List<string>() {
        "Tap a line",
        "to change its",
        "contents",
        "Drag inside a block",
        "to change its color",
        "Double tap a block",
        "to select its color",
        "for the day"
    };

    public int colorCount = 8;

    void Init()
    {
        // CheckSavedValues();
		CreatePalette();
    }

    void Start()
    {
        Init();
    }

    void CreatePalette()
    {
        float w = ScreenInfo.w;
        float h = ScreenInfo.h / colorCount;

        for (int i = 0; i < colorCount; i++)
        {
            UnityEngine.UI.InputField newInput = Instantiate(input);
            allInputs.Add(newInput);
            newInput.transform.parent = transform;
            newInput.GetComponent<RectTransform>().position = new Vector3(0, ScreenInfo.h - h * i - h / 2 - ScreenInfo.h / 2, 0);
            newInput.transform.localScale = Vector2.one;
            newInput.transform.Find("Sprite").localScale = new Vector2(ScreenInfo.x, ScreenInfo.y / colorCount);
            newInput.GetComponent<InputController>().fieldIndex = i;

            GameObject p = newInput.transform.Find("Placeholder").gameObject;
            p.GetComponent<Text>().text = instructionsText[i];

            userText.Add(newInput.transform.Find("Text").GetComponent<Text>());
            savedText.Add("");
        }

		hideableText = transform.GetComponentsInChildren<TextFader>().ToList();
    }

    public IEnumerator ShowGlossary(bool val)
    {
		foreach(TextFader t in  hideableText) {
			StartCoroutine(t.ShowText(val));
			yield return Extensions.Wait(.05f);
		}
    }

    // List<string> userText = new List<string>();
    List<Text> userText = new List<Text>();
    List<string> savedText = new List<string>();

    public void SaveUserText(int index, string content) {
        userText[index].text = content;
        savedText[index] = content; 


        
        // TODO: MAKE THIS ACTUALLY SAVE FOREVER
        GlobalSettings.GetInstance().UpdateText(savedText);
    }

    public void ShowInstructions(bool val) {
        if(val) {
            Debug.Log("Showing instructin");

        }


        for(int i = 0; i <userText.Count; i++) {
            allInputs[i].text = val ? "" : savedText[i] ;
        }
    }
}
