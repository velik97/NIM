using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BaseMenuManager : MonoSingleton <BaseMenuManager> {

	public Theme menuTheme;
	public bool isInMenu;
	public bool win;

	public Stack <BasicMenu> menuStack;

	public virtual void Start () {
		menuTheme = CrossDataHandler.CurrentTheme;
		isInMenu = false;
		menuStack = new Stack<BasicMenu>();
	}

	public void OpenMenu (BasicMenu menu) {
		isInMenu = true;
		menuStack.Push (menu);
		menu.Appear ();
	}

	public void CloseTopMenu () {
		if (menuStack.Count == 0) {
			Debug.LogError ("[MenuManager] You're trying to close menu, but menu stack is empty");
			return;
		}

		BasicMenu topMenu = menuStack.Pop ();
		topMenu.Disappear ();
		if (menuStack.Count == 0) {
			Invoke ("ActivateGameField", .5f);
		}
	}

	public void CloseAllMenus () {
		if (menuStack.Count == 0) {
			Debug.LogError ("[MenuManager] You're trying to close menu, but menu stack is empty");
			return;
		}

		while (menuStack.Count > 0) {
			BasicMenu topMenu = menuStack.Pop ();
			topMenu.Disappear ();
		}
		if (menuStack.Count == 0) {
			Invoke ("ActivateGameField", .5f);
		}
	}

	public void ActivateGameField () {
		isInMenu = false;
	}

	public virtual void GameOver (bool won) {
		win = won;
		return;
	}

	public virtual void GameOver (int player) {
		return;
	}

	public void Restart () {
		CloseEverything ();
		Invoke ("RebootScene", .8f);
	}

	public void GoToMenu() {
		CloseEverything ();
		Invoke ("OpenMenuScene", .8f);
	}

	public virtual void CloseEverything () {
		Map.Instance.Disappear ();
		BaseGameManager.Instance.gameHeader.DisAppear ();
		CloseAllMenus ();
	}

	public virtual void RebootScene () {
		CrossDataHandler.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void OpenMenuScene () {
		CrossDataHandler.LoadScene (0);
	}


}
