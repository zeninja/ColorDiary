using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

	public static void SaveInfo(GlobalSettings info) {
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/PlayerData.txt";

		FileStream stream = new FileStream (path, FileMode.Create);
		GameData   data   = new GameData   (info);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static GameData LoadInfo() {
		string path = Application.persistentDataPath + "/PlayerData.txt";

		if(File.Exists(path)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream (path, FileMode.Open);
			GameData data = formatter.Deserialize(stream) as GameData;
			stream.Close();

			return data;
		} else {
			Debug.Log("No path");
			return null;
		}

	}

}
