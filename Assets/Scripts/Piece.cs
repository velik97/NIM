using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

	public SpriteRenderer insideSpriteRenderer;
	public Animator animator;
	public bool active;
	public bool takenOut;
	public Row adultRow;

	void Start () {
		active = false;
		takenOut = false;
	}

	public void SetColor(Color color) {
		insideSpriteRenderer.color = color;
	}

	public void Mark (Sprite sprite) {
		StartCoroutine (IMark (sprite));
	}

	IEnumerator IMark (Sprite sprite) {
		animator.SetTrigger ("Shake");
		yield return new WaitForSeconds (.1f);
		insideSpriteRenderer.sprite = sprite;
	}

	public void Tap () {
		if (!takenOut) {
			if (!active) {
				Chose ();
				adultRow.Tap ();
			} else {
				Unchose ();
				adultRow.CheckActivity ();
			}
		}
	}

	public void Chose () {
		animator.SetBool ("Chosen", true);
		active = true;
	}

	public void TakeOut () {
		takenOut = true;
		active = false;
		animator.SetTrigger ("Take Out");
	}

	public void Unchose () {
		animator.SetBool ("Chosen", false);
		active = false;
	}

	public void Shake () {
		animator.SetTrigger ("Shake");
	}
}
