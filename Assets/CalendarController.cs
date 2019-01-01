using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalendarController : MonoBehaviour {

	int freqIndex = 0;
	float frequency;
	List<float> frequencies = new List<float>() { 5, 30, 60, 360, 960,  }; // in minutes.  960 = 16 hrs
	string frequencyString;

	// public TextMeshProUGUI frequencyDisplay;
	// public TextMeshProUGUI timeLeft;

	void Start() {
		frequency = frequencies[freqIndex];
		UpdateFrequencyString();
	}

	void Update() {
		// UpdateMessage();
	}

	// void UpdateMessage() {
	// 	frequencyDisplay.text  = frequencyString;
	// }

	public void IncrementFrequency() {
		freqIndex = (freqIndex + 1) % frequencies.Count;
		frequency = frequencies[freqIndex];

		UpdateFrequencyString();
	}

	void UpdateFrequencyString() {
		if(frequency == frequencies[0]) {
			frequencyString = "5 minutes";
		} else if(frequency == frequencies[1]) {
			frequencyString = "30 minutes";
		} else if(frequency == frequencies[2]) {
			frequencyString = "1 hour";
		} else if(frequency == frequencies[3]) {
			frequencyString = "6 hours";
		} else if(frequency == frequencies[4]) {
			frequencyString = "16 hours";
		}
	}

	float lastSelectionTime;

	public void SetLastSelectionTime() {
		lastSelectionTime = Time.time;
		
	}
}
