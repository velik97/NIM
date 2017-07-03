using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMap : Map {

	public MapConfig firstMapConfig;
	public MapConfig secondMapConfig;

	public Text hintText;

	public Animator hintAnimator;

	public override void AssembleConfigs () {
		theme = CrossDataHandler.themeSet.themes [CrossDataHandler.CurrentThemeIndex];

		int level = CrossDataHandler.TutorialLevel;
		if (level < 0 || level > 1) {
			Debug.LogError ("[TutorialMap] 'TutorialLevel' field doesn't fit");
		} else {
			if (level == 0) {
				mapConfig = firstMapConfig;
				hintText.text = "Go last\nto win";
			} else {
				mapConfig = secondMapConfig;
				hintText.text = "Go last\nto win";
			}
			hintText.color = theme.Lerp (.5f);
			hintText.gameObject.SetActive (false);
		}
	}

	public override IEnumerator IAppear () {
		GoButton.Instance.GetComponent <Animator> ().SetBool ("Appear", true);
		for (int r = 0; r < rows.Length; r++) {
			rows [r].Appear ();
			yield return new WaitForSeconds (.1f);
		}
		yield return new WaitForSeconds (.2f);
		hintText.gameObject.SetActive (true);
		hintAnimator.SetBool ("Appear", true);
		BaseGameManager.Instance.StartGame ();
	}

	public override IEnumerator IDisappear () {
		hintAnimator.SetBool ("Appear", false);
		GoButton.Instance.GetComponent <Animator> ().SetBool ("Appear", false);
		for (int r = rows.Length - 1; r >= 0; r--) {
			rows [r].Disappear ();
			yield return new WaitForSeconds (.1f);
		}
	}
}
