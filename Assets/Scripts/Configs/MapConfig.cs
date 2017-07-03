using UnityEngine;
using System.Collections;

[System.Serializable]
public class MapConfig {

	public int[] rowsLength;

	public MapConfig (int[] rowsLength) {
		this.rowsLength = rowsLength;
	}

	public override string ToString () {
		string str;
		str = rowsLength [0].ToString ();

		for (int i = 1; i < rowsLength.Length; i++) {
			str += " " + rowsLength [i].ToString ();
		}

		return str;
	}

}
