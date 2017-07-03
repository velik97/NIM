using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialMenuManager : BaseMenuManager {

	public SimpleMenu winMenu;
	public SimpleMenu loseMenu;

	public override void Start () {
		base.Start ();
		winMenu.SetTheme (menuTheme);
		loseMenu.SetTheme (menuTheme);
	}

	public override void GameOver (bool won) {
		base.GameOver (won);
		if (won) {
			OpenMenu (winMenu);
		} else {
			OpenMenu (loseMenu);
		}

	}

	public override void CloseEverything () {
		base.CloseEverything ();
	}

	public override void RebootScene () {
		if (win) {
			int level = CrossDataHandler.TutorialLevel;
			level++;
			if (level > 1) {
				CrossDataHandler.PassedTutorial = 1;
				CrossDataHandler.LoadScene (0);
			} else {
				CrossDataHandler.TutorialLevel = level;
				CrossDataHandler.LoadScene (2);
			}
		} else {
			SceneManager.LoadScene (2);
		}
	}

}
