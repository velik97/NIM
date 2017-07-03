using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Header : MonoBehaviour {

	public Text headerText;
	public Animator headerAnimator;
	public Image headerImage;

	public float replaceTextDelay;

	public Color color;

	public void SetText (string text) {
		if (headerText)
			headerText.text = text;
	}

	public void SetColor (Color color) {
		if (headerText)
			headerText.color = color;
		if (headerImage)
			headerImage.color = color;
		this.color = color;
	}



	public void Appear () {
		gameObject.SetActive (true);
		headerAnimator.SetBool ("Appear", true);
	}



	public void DisAppear () {
		StartCoroutine (IDisappear ());
	}

	private IEnumerator IDisappear () {
		headerAnimator.SetBool ("Appear", false);
		yield return new WaitForSeconds (.2f);
		gameObject.SetActive (false);
	}



	public void ReplaceText (string text, Color color) {
		StartCoroutine (IReplaceText (text, color));
	}

	private IEnumerator IReplaceText (string text, Color color) {
		headerAnimator.SetTrigger ("Shake");
		yield return new WaitForSeconds (replaceTextDelay);
		SetText (text);
		SetColor (color);
//		headerAnimator.gameObject.SetActive (true);
//		headerAnimator.SetBool ("Appear", true);
	}

	public void ReplaceText (string text) {
		StartCoroutine (IReplaceText (text));
	}

	private IEnumerator IReplaceText (string text) {
		headerAnimator.SetTrigger ("Shake");
		yield return new WaitForSeconds (replaceTextDelay);
		SetText (text);
//		headerAnimator.gameObject.SetActive (true);
//		headerAnimator.SetBool ("Appear", true);
	}

}
