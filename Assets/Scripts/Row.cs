using UnityEngine;
using System.Collections;

public class Row : MonoBehaviour {

	public Piece[] pieces;
	public Transform sub;
	public bool active;
	public bool empty;
	public Color pieceColor;
	public AudioSource sound;
	public Animator animator;
	public SpriteRenderer sr;
	public float fadeTime = .5f;

	public float startActivatePitch;
	public float activatePitchStep;

	private float activatePitch;
	public float disactivatePitch;

	public void Tap() {
		Map.Instance.Tap (this as Row, pieceColor);
		active = true;
		activatePitch = startActivatePitch - activatePitchStep;
		foreach (Piece piece in pieces) {
			if (piece.active)
				activatePitch += activatePitchStep;
		}
		sound.pitch = activatePitch;
		sound.Play ();
	}

	public void CheckActivity () {
		active = false;
		foreach (Piece piece in pieces) {
			if (piece.active) {
				active = true;
			}
		}
		if (!active)
			Map.Instance.Disactivate ();

		sound.pitch = disactivatePitch;
		sound.Play ();
	}

	public void DisactivateAll() {
		foreach (Piece piece in pieces) {
			if (piece.active) {
				piece.Unchose ();
			}
		}
		active = false;
	}

	public void TakeOut() {
		empty = true;

		foreach (Piece piece in pieces) {
			if (piece.active && !piece.takenOut) {
				piece.TakeOut ();
			}
			if (!piece.takenOut) {
				empty = false;
			}
		}
			
		active = false;
		if (empty)
			StartCoroutine (FadeOut ());
	}

	IEnumerator FadeOut () {
		float offsetTime = 0f;
		Color startColor = sr.color;
		Color endColor = startColor;
		endColor.a = 0f;
		while (offsetTime < fadeTime) {
			SetColor (Color.Lerp (startColor, endColor, offsetTime / fadeTime));
			offsetTime += Time.deltaTime;
			yield return null;
		}
		SetColor (endColor);
	}

	public void Appear () {
		animator.SetBool ("Up", true);
		animator.SetBool ("Appear", true);
	}

	public void Disappear () {
		animator.SetBool ("Appear", false);
	}

	public void Shake () {
		for (int i = 0; i < pieces.Length; i++) {
			if (!pieces [i].takenOut) {
				pieces [i].Shake ();
			}
		}
	}

	public void SetColor (Color color) {
		sr.color = color;
	}
}
