using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : BaseMenuManager {

	public BasicMenu sideMenu;
	public SimpleMenu pauseMenu;
	public MenuWithText menuWithText;
	public MenuWithContentButton preWinMenu;
	public MenuWithContentButton winMenu;
	public SimpleMenu takeHintsMenu;
	public SimpleMenu noWayToWinMenu;
	public SimpleMenu TransactionResaultMenu;
	public Header hintsCountHeader;


	public override void Start () {
		base.Start ();
		ApplyTheme (menuTheme);
		hintsCountHeader.SetText (CrossDataHandler.HintsCount.ToString ());
	}

	public void ApplyTheme (Theme theme) {
		Hint.Instance.SetColor (theme.startColor);
		sideMenu.SetTheme (theme);
		pauseMenu.SetTheme (theme);
		menuWithText.SetTheme (theme);
		preWinMenu.SetTheme (theme);
		winMenu.SetTheme (theme);
		takeHintsMenu.SetTheme (theme);
		noWayToWinMenu.SetTheme (theme);
		TransactionResaultMenu.SetTheme (theme);
		hintsCountHeader.SetColor (theme.startColor);
	}

	public void SetNewTheme () {
		StartCoroutine (IApplyTheme ());
	}

	IEnumerator IApplyTheme () {
		float time = 0f;
		Theme prevTheme = CrossDataHandler.CurrentTheme;
		CrossDataHandler.CurrentThemeIndex = CrossDataHandler.MaxAvailableThemeIndex;
		CrossDataHandler.Serialize ();
		Theme currentTheme = CrossDataHandler.CurrentTheme;

		Color prevGoButtonColor = GoButton.Instance.buttonImage.color;
		Color prevHeaderColor = GameManager.Instance.gameHeader.color;
		while (time < .2f) {
			Theme intermediateTheme = Theme.ThemeLerp (prevTheme, currentTheme, time * 5f);
			preWinMenu.SetTheme (intermediateTheme);
			GameManager.Instance.gameHeader.color = Color.Lerp (prevHeaderColor, intermediateTheme.endColor, time * 5f);
			GoButton.Instance.buttonImage.color = Color.Lerp (prevGoButtonColor, intermediateTheme.startColor, time * 5f);
			Hint.Instance.SetColor (Color.Lerp (prevTheme.startColor, currentTheme.startColor, time * 5f));
			time += Time.deltaTime;
			yield return null;
		}

		preWinMenu.SetTheme (currentTheme);
		winMenu.SetTheme (currentTheme);
		menuWithText.SetTheme (currentTheme);
		GameManager.Instance.gameHeader.SetColor (currentTheme.endColor);
		Hint.Instance.SetColor (currentTheme.startColor);
		GoButton.Instance.buttonImage.color = currentTheme.startColor;

	}

	public override void GameOver (bool won) {
		isInMenu = true;
			
		if (won) {

			int maxAvailableLevel = CrossDataHandler.MaxAvailableLevel;
			
			if (CrossDataHandler.CurrentLevel == maxAvailableLevel) {

				UpdateInfoAfterLevelWin ();
				OpenMenu (preWinMenu);

			} else {

				if (CrossDataHandler.CurrentLevel == CrossDataHandler.mapConfigSet.mapConfigs.Length - 1) {
					
					menuWithText.SetText ("You won!");
					OpenMenu (menuWithText);
				} else {
					
					winMenu.SetUnlocked ();
					OpenMenu (winMenu);
				}
			}
		} else {
			
			menuWithText.SetText ("You lose...");
			OpenMenu (menuWithText);
		}
	}

	void UpdateInfoAfterLevelWin () {

		CrossDataHandler.WinTimes++;
		if (CrossDataHandler.WinTimes == 3) {
			CrossDataHandler.WinTimes = 0;
			CrossDataHandler.MaxAvailableLevel++;
			CrossDataHandler.MaxAvailableThemeIndex++;
		}
	}

	public void GoToWinMenu () {
		StartCoroutine (IGoToWinMenu ());
	}

	public void GoToNextLevel () {
		CrossDataHandler.CurrentLevel++;
		Restart ();
	}

	IEnumerator IGoToWinMenu () {
		CloseTopMenu ();
		yield return new WaitForSeconds (.25f);

		if (CrossDataHandler.CurrentLevel == CrossDataHandler.mapConfigSet.mapConfigs.Length - 1) {
			
			if (CrossDataHandler.WinTimes == 0) {

				menuWithText.SetText ("You won!");
				OpenMenu (menuWithText);
			} else {

				OpenMenu (winMenu);
			}
		} else {

			OpenMenu (winMenu);
		}

	}

	public override void GameOver (int player) {
		menuWithText.SetText ("Player " + player + " won!");
		OpenMenu (menuWithText);
	}

	public override void CloseEverything () {
		CloseAllMenus ();
		Map.Instance.Disappear ();
		BaseGameManager.Instance.gameHeader.DisAppear ();
	}
		
}
