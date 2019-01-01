using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calendar : MonoBehaviour {

	class Month { 
		int length;
		public List<Color> dayColors;
		List<string> notes;

		public Month(int m) {
			length = m;
			dayColors = new List<Color>(m);
			notes = new List<string>(m);
			// Debug.Log(m);
			Init();
			// Debug.Log("days: " + days.Count);
		}

		public int GetLength() {
			return length;
		}
 		void Init() {
			dayColors  = new List<Color> (  new Color[length] );
			notes = new List<string>( new string[length] );

			// Debug.Log("l: " + length + "; days: " + days.Count);
		}
	}

	public static void AddColor(int dayIndex, Color color) {
		int monthIndex = 0;
		while(dayIndex > year[monthIndex].GetLength() - 1) {
			dayIndex -=  year[monthIndex].GetLength();
			monthIndex++;
		}
		year[monthIndex].dayColors[dayIndex] = color;
		Calendar.UpdateBlocks();
	}

	public static int totalDayCount = 365;

	static Month[] year = new Month[] {
		new Month(31), // J
		new Month(28), // F
		new Month(31), // M
		new Month(30), // A
		new Month(31), // M
		new Month(30), // J
		new Month(31), // J
		new Month(31), // A
		new Month(30), // S
		new Month(31), // O
		new Month(30), // N
		new Month(31)  // D
	};

	public ColorBlock dayblock;
	int cols = 12, rows = 31;

	void Start() {
		InitRenderer();
	}

	void InitRenderer() {
		for(int c = 0; c < cols; c++) {
			for(int r = 0; r < rows; r++) {

				float x = ScreenInfo.w / cols;
				float y = ScreenInfo.h / rows;

				Vector2 scale = new Vector2(x, y);
				Vector2 pos   = new Vector2( ScreenInfo.w * c / cols - ScreenInfo.w / 2, ScreenInfo.h - ScreenInfo.h * r / rows - ScreenInfo.h / 2);  
				pos += new Vector2(x/2, -y/2);

				ColorBlock b = Instantiate(dayblock, pos, Quaternion.identity);
				blocks.Add(b);
				b.transform.localScale = scale;
				b.transform.parent = transform;
			}
		}
	}

	static List<ColorBlock> blocks = new List<ColorBlock>();

	static void UpdateBlocks() {
		int daysElapsed = 0;
		foreach (Month m in year) {
			for (int i = 0; i < m.dayColors.Count; i++) {
				if (m.dayColors[i].a != 0) {
					blocks[daysElapsed + i].SetColor(m.dayColors[i]);
					blocks[daysElapsed + i].SetSelected(true);

					lastSelectedBlock = blocks[daysElapsed + i];
				}
			}
			daysElapsed += 31; // 31 blocks are spawned per row, even if the month has fewer days. add 31 days to skip the extra unused slots
		}
	}

	static ColorBlock lastSelectedBlock;
}
