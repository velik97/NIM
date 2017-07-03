using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class InverseStrategy : MonoSingleton <InverseStrategy>, IStrategy {

	public int[] maxValues;
	public List <int[]> losePositions;

	public void Prepare (int[] mapValues) {
		maxValues = Sort (mapValues);
		FindLosePositions ();
	}

	void Start () {
//		foreach (int[] losePosition in losePositions) {
//			PrintRows (losePosition);
//		}
	}

	public int ShowSuccessfullWay (int[] currentPos) {
		int[] sortedCurrentPos = Sort (currentPos);

		List <int[]> possibleWays = new List<int[]> ();
		foreach (int[] possibleWay in PossibleToGoFrom (sortedCurrentPos)) {
			if (LosePositionsContains (possibleWay)) {
				possibleWays.Add (possibleWay);
			}
		}

		foreach (int[] possibleWay in possibleWays) {
			PrintRows (possibleWay);
		}

		int random;

		if (possibleWays.Count > 0) {
			random = Random.Range (0, possibleWays.Count);
			int[] winPos = possibleWays [random];
			List <int> prevPosList = CopyToList (sortedCurrentPos);
			List <int> currentPosList = CopyToList (winPos);

			while (currentPosList.Count > 1) {
				for (int i = 0; i < prevPosList.Count; i++) {
					for (int j = 0; j < currentPosList.Count; j++) {
						if (prevPosList [i] == currentPosList [j]) {
							prevPosList.RemoveAt (i);
							currentPosList.RemoveAt (j);
						}
					}
				}
			}

			int valueDif = prevPosList [0] - currentPosList [0];
			int changedValue = prevPosList [0];

			int changedPos = 0;
			while (changedPos < currentPos.Length && currentPos [changedPos] != changedValue) {
				changedPos++;
			}

			return (changedPos + 1) * 10 + valueDif;
		}

		random = Random.Range (0, sortedCurrentPos.Length);

		while (currentPos [random] == 0) {
			random = Random.Range (0, currentPos.Length);
		}

		return (random + 1) * 10 + Random.Range (0, currentPos [random]) + 1;
	}

	void FindLosePositions () {
		losePositions = new List <int[]> ();

		int[] currentRow;

		bool contains;

		losePositions = new List<int[]> ();

		for (int l = 1; l <= maxValues [0]; l++) {
			currentRow = new int[maxValues.Length];
			currentRow [0] = l;
			for (int i = 1; i < maxValues.Length; i++) {
				currentRow [i] = 0;
			}

			connectedSubPositions = new List<int[]> ();
			FindConnectedSubPositions (currentRow);

			foreach (int[] connectedRows in connectedSubPositions) {
				contains = false;
				foreach (int[] possibleRows in PossibleToGoFrom (connectedRows)) {
					if (LosePositionsContains (possibleRows)) {
						contains = true;
						break;
					}
				}
				if (!contains)
					losePositions.Add (connectedRows);
			}
		}

	}

	private List <int[]> connectedSubPositions;

	void FindConnectedSubPositions (int[] upPositions) {
		int upPositionsLength = 0;
		int[] currentRow = Copy (upPositions);

		for (int i = 0; i < upPositions.Length; i++) {
			if (upPositions [i] != 0)
				upPositionsLength++;
		}
			
		connectedSubPositions.Add (Copy (upPositions));

		if (! (upPositionsLength == maxValues.Length)) {
			while (currentRow [upPositionsLength] < maxValues [upPositionsLength] && currentRow [upPositionsLength] < currentRow [upPositionsLength - 1]) {
				currentRow [upPositionsLength]++;
				FindConnectedSubPositions (currentRow);
			}
		}
	}

	List <int[]> PossibleToGoFrom (int[] fromRow) {
		List <int[]> possibleRows = new List<int[]> ();
		List <int> containingLengths = new List <int> ();

		for (int r = 0; r < fromRow.Length; r++) {
			if (!containingLengths.Contains (fromRow [r])) {
				containingLengths.Add (fromRow[r]);

				for (int l = 0; l < fromRow [r]; l++) {
					int[] possibleRow = Copy (fromRow);
					possibleRow [r] = l;
					possibleRows.Add (Sort (possibleRow));
				}
			}
		}

		return possibleRows;
	}

	int [] Copy (int [] array) {
		int[] copy = new int[array.Length];

		for (int i = 0; i < array.Length; i++) {
			copy [i] = array [i];
		}

		return copy;
	}

	List <int> CopyToList (int [] array) {
		List <int> copy = new List<int>();

		for (int i = 0; i < array.Length; i++) {
			copy.Add (array [i]);
		}

		return copy;
	}

	bool LosePositionsContains (int[] row) {
		bool contains = false;

		foreach (int[] r in losePositions) {
			if (RowsAreEqual (row, r)) {
				contains = true;
				break;
			}
		}

		return contains;
	}

	bool RowsAreEqual (int[] a, int[] b) {
		if (a.Length == b.Length) {
			bool equal = true;
			for (int i = 0; i < a.Length; i++) {
				if (a [i] != b [i]) {
					equal = false;
					break;
				}
			}

			return equal;
		} else {
			return false;
		}
	}

	int [] Sort (int [] rows) {
		int[] sorted = Copy (rows);

		for (int i = rows.Length - 1; i > 0; i--) {
			for (int j = 0; j < i; j++) {
				if (sorted [j + 1] > sorted [j]) {
					int t = sorted [j];
					sorted [j] = sorted [j + 1];
					sorted [j + 1] = t;
				}
			}
		}

		return sorted;
	}

	void PrintRows (int[] row) {
		StringBuilder newString = new StringBuilder();

		foreach (int i in row) {
			newString.Append (i.ToString ());
		}
	}
		

}
