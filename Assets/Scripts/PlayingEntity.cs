using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public abstract class PlayingEntity : MonoBehaviour {

	public abstract void StartStep ();

	public abstract void EndStep ();

	protected void Go (Piece piece) {
		piece.Tap ();
	}

	public static void DisActivateMap () {
		Map.Instance.Disactivate ();
	}
}
