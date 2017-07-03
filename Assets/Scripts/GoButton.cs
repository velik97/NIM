using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoButton : MonoSingleton <GoButton> {

	private Color previousButtonColor;
	private Color previousBarColor;
	public float colorIntence;

	public Image buttonImage;
	public Button button;

	public AudioSource sound;
	public Image barImage;

	public bool active;

	public float setTime;
	private float offsetTime;

	void Start () {
		active = false;
	}

	public void Activate (Color color) {
		active = true;
		StartCoroutine (ISetButtonColor (color));
		StartCoroutine (ISetBarColor (new Color (1, 1, 1, .2f)));
	}

	public void Disactivate () {
		active = false;
		StartCoroutine (ISetButtonColor (Color.Lerp (new Color (1, 1, 1, 0), buttonImage.color , colorIntence)));
		StartCoroutine (ISetBarColor (new Color (1, 1, 1, 0)));
	}

	IEnumerator ISetButtonColor (Color color) {
		offsetTime = 0f;
		previousButtonColor = buttonImage.color;
		while (offsetTime < setTime) {
			buttonImage.color = Color.Lerp (previousButtonColor, color, offsetTime / setTime);
			yield return null;
			offsetTime += Time.deltaTime;
		}
		buttonImage.color = color;
	}

	IEnumerator ISetBarColor (Color color) {
		offsetTime = 0f;
		previousBarColor = barImage.color;
		while (offsetTime < setTime) {
			barImage.color = Color.Lerp (previousBarColor, color, offsetTime / setTime);
			yield return null;
			offsetTime += Time.deltaTime;
		}
		barImage.color = color;
	}

	public void React () {
		if (Map.Instance.active) {
			Go ();
			Disactivate ();
		} else {
			Map.Instance.Shake ();
		}
	}

	public void Go () {
		bool mapIsEmpty = true;
		sound.Play ();

		foreach (Row row in Map.Instance.rows) {
			if (row.active) {
				row.TakeOut ();
			}
			if (!row.empty) {
				mapIsEmpty = false;
			}
		}

		if (Hint.Instance) {
			Hint.Instance.HintIsShown = false;
		}

		Map.Instance.active = false;

		if (!mapIsEmpty)
			BaseGameManager.Instance.NextGo ();
		else
			BaseGameManager.Instance.GameOver ();
	}
}
