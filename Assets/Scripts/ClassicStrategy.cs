using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicStrategy {

	public static int ShowSuccessfulWay (int[] currentPos) {
		int nimSum = NimSum (currentPos);

		if (nimSum == 0) {
			int random = Random.Range (0, currentPos.Length);

			while (currentPos [random] == 0) {
				random = Random.Range (0, currentPos.Length);
			}

			return (random + 1) * 10 + Random.Range (0, currentPos [random]) + 1;

		} else {
			List <int> successfulWays = new List<int>();

			for (int i = 0; i < currentPos.Length; i++) {
				for (int j = 1; j <= currentPos [i]; j++) {
					int[] way = Copy (currentPos);
					way [i] -= j;
					if (NimSum (way) == 0) {
						successfulWays.Add ((i + 1) * 10 + j);
					}
				}
			}

			if (successfulWays.Count > 0) {
				return successfulWays [Random.Range (0, successfulWays.Count)];
			} else {
				Debug.LogError ("[Classic Strategy]: no successful way found");
				return 0;
			}
		}
	}

	public static int ShowSuccessfulWay (int[] currentPos, out bool wayExists) {
		int nimSum = NimSum (currentPos);

		if (nimSum == 0) {
			int random = Random.Range (0, currentPos.Length);

			while (currentPos [random] == 0) {
				random = Random.Range (0, currentPos.Length);
			}

			wayExists = false;
			return (random + 1) * 10 + Random.Range (0, currentPos [random]) + 1;

		} else {
			List <int> successfulWays = new List<int>();

			for (int i = 0; i < currentPos.Length; i++) {
				for (int j = 1; j <= currentPos [i]; j++) {
					int[] way = Copy (currentPos);
					way [i] -= j;
					if (NimSum (way) == 0) {
						successfulWays.Add ((i + 1) * 10 + j);
					}
				}
			}
				
			if (successfulWays.Count > 0) {
				wayExists = true;
				return successfulWays [Random.Range (0, successfulWays.Count)];
			} else {
				wayExists = true;
				Debug.LogError ("[Classic Strategy]: no successful way found");
				return 0;
			}
		}
	}
		

	static int [] Copy (int [] array) {
		int[] copy = new int[array.Length];

		for (int i = 0; i < array.Length; i++) {
			copy [i] = array [i];
		}

		return copy;
	}

	static int NimSum (int[] addendums) {
		int nimSum = 0;
		for (int i = 0; i < addendums.Length; i++) {
			nimSum ^= addendums [i];
		}

		return nimSum;
	}
}
