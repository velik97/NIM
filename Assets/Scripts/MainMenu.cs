using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

	public enum Type {MapConfig, Theme, Players};
	private Type currentMenu;

	public int winRate = 3;

	[Header("Used Data")]
	public MenuButtonWithAnimation1 buttonPrefab;
	public Sprite lockSprite;
	public Sprite doneSprite;

	[Header("References")]
	public Header header;
	public Animator backButtonAnimator;

	[Header("Timings")]
	public float timeBetweenButtons = .1f;
	public float timeToChangeColor = .1f;
	private string configText = "";

	[Header("Holders")]
	public Transform configButtonsHolder;
	public Transform themeButtonsHolder;
	public Transform playersCountButtonsHolder;

	private MenuButtonWithAnimation1[] configButtons;
	private MenuButtonWithAnimation1[] themeButtons;
	private MenuButtonWithAnimation1[] playerCountButtons;

	void Awake () {
		CrossDataHandler.Deserialize ();
//		CrossDataHandler.Reboot ();
//		CrossDataHandler.MaxAvailableLevel = 0;
//		CrossDataHandler.MaxAvailableThemeIndex = 0;
//		CrossDataHandler.WinTimes = 2;

		CheckForTutorial ();
	}

	void Start () {
		Generate ();
		Theme theme = CrossDataHandler.CurrentTheme;
		if (theme == null)
			ChangeTheme (0);
		else
			ChangeTheme (theme);

		currentMenu = Type.MapConfig;
		header.SetText ("");
		StartCoroutine (IGoToConfigChoice ());
	}

	void CheckForTutorial () {
		if (CrossDataHandler.PassedTutorial == 0) {
			SwitchToTutorialScene ();
		}
	}

	public void GoToTutorial () {
		StartCoroutine (IGoToTutorial ());
	}

	void SwitchToTutorialScene () {
		CrossDataHandler.TutorialLevel = 0;
		CrossDataHandler.Serialize ();
		SceneManager.LoadScene (2);
	}

	public void ExecuteButton (Type type, int index) {
		if (type == Type.MapConfig) {
			if (index < CrossDataHandler.mapConfigSet.mapConfigs.Length) {
				configText = configButtons [configButtons.Length - index - 1].text.text;
				CrossDataHandler.CurrentLevel = index;
				if (index == CrossDataHandler.MaxAvailableLevel) {
					playerCountButtons [playerCountButtons.Length - 1].SetText ("Computer");
					playerCountButtons [playerCountButtons.Length - 1].SetInfo (CrossDataHandler.WinTimes + "/" + winRate);
				} else {
					playerCountButtons [playerCountButtons.Length - 1].SetText ("Computer");
					playerCountButtons [playerCountButtons.Length - 1].SetIcon (doneSprite);
				}

				StartCoroutine (IGoToPlayerCountChoice ());
			} else if (index == CrossDataHandler.mapConfigSet.mapConfigs.Length) {
				StartCoroutine (IGoToTutorial ());
			} else {
				StartCoroutine (IGoTothemes ());
			}
		} else if (type == Type.Theme) {
			ChangeTheme (index);
		} else if (type == Type.Players) { 
			CrossDataHandler.PlayersCount = index;
			StartCoroutine (IPlay ());
		}
	}

	IEnumerator IGoToTutorial () {
		header.DisAppear ();
		for (int i = 0; i < configButtons.Length; i++) {
			configButtons [i].DisAppear (false);
			yield return new WaitForSeconds (timeBetweenButtons);
		}
		yield return new WaitForSeconds (.6f);
		SwitchToTutorialScene ();
	}

	IEnumerator IGoTothemes () {
		header.DisAppear ();
		for (int i = configButtons.Length - 1; i >= 0; i--) {
			configButtons [i].DisAppear (true);
			yield return new WaitForSeconds (timeBetweenButtons);
		}

		for (int i = themeButtons.Length - 1; i >= 0; i--) {
			themeButtons [i].Appear (false);
			yield return new WaitForSeconds (timeBetweenButtons);
		}

		backButtonAnimator.gameObject.SetActive (true);
		backButtonAnimator.SetBool ("Appear", true);
		header.SetText ("Themes");
		header.Appear ();
		currentMenu = Type.Theme;
	}

	public void GoToConfigChoice () {
		StartCoroutine (IGoToConfigChoice ());
	}

	IEnumerator IGoToConfigChoice () {
		backButtonAnimator.SetBool ("Appear", false);

		if (currentMenu == Type.Theme) {
			header.DisAppear ();
			for (int i = 0; i < themeButtons.Length; i++) {
				themeButtons [i].DisAppear (false);
				yield return new WaitForSeconds (timeBetweenButtons);
			}
		} else if (currentMenu == Type.Players) {
			header.DisAppear ();
			for (int i = playerCountButtons.Length -1; i >= 0; i--) {
				playerCountButtons [i].DisAppear (true);
				yield return new WaitForSeconds (timeBetweenButtons);
			}
		}
		backButtonAnimator.gameObject.SetActive (false);

		if (currentMenu == Type.Players) {
			for (int i = configButtons.Length - 1; i >= 0; i--) {
				configButtons [i].Appear (false);
				yield return new WaitForSeconds (timeBetweenButtons);
			}
		} else {
			for (int i = 0; i < configButtons.Length; i++) {
				configButtons [i].Appear (true);
				yield return new WaitForSeconds (timeBetweenButtons);
			}
		}
		header.SetText ("NIM");
		header.Appear ();
		currentMenu = Type.MapConfig;
	}

	IEnumerator IGoToPlayerCountChoice () {
		header.DisAppear ();
		for (int i = 0; i < configButtons.Length; i++) {
			configButtons [i].DisAppear (false);
			yield return new WaitForSeconds (timeBetweenButtons);
		}
			
		for (int i = 0; i < playerCountButtons.Length; i++) {
			playerCountButtons [i].Appear (true);
			yield return new WaitForSeconds (timeBetweenButtons);
		}

		header.SetText (configText);
		header.Appear ();
		currentMenu = Type.Players;
		backButtonAnimator.gameObject.SetActive (true);
		backButtonAnimator.SetBool ("Appear", true);
	}

	IEnumerator IPlay () {
		backButtonAnimator.SetBool ("Appear", false);
		header.DisAppear ();
		for (int i = 0; i < playerCountButtons.Length; i++) {
			playerCountButtons [i].DisAppear (false);
			yield return new WaitForSeconds (timeBetweenButtons);
		}
		yield return new WaitForSeconds (.6f);
		CrossDataHandler.LoadScene (1);
	}

	public void ChangeTheme (int index) {
		MenuButtonWithAnimation1 currentMenuButton = themeButtons [themeButtons.Length - CrossDataHandler.CurrentThemeIndex - 1];
		currentMenuButton.SetInfo("");
		CrossDataHandler.CurrentThemeIndex = index;
		CrossDataHandler.Serialize ();
		currentMenuButton = themeButtons [themeButtons.Length - CrossDataHandler.CurrentThemeIndex - 1];
		currentMenuButton.SetIcon (doneSprite);
		ChangeTheme (CrossDataHandler.CurrentTheme);
	}

	void ChangeTheme (Theme theme) {
		ApplyTheme (configButtons, theme);
		ApplyTheme (playerCountButtons, theme);
		StartCoroutine (IApplyTheme (themeButtons, theme));
		StartCoroutine (IApplyTheme (header.headerText, theme));
		StartCoroutine (IApplyTheme (backButtonAnimator.GetComponentInChildren <Image> (), theme));
	}

	IEnumerator IApplyTheme (MenuButtonWithAnimation1[] buttons, Theme theme) {
		float offset = 0f;
		Color[] previousColors = new Color [buttons.Length];

		for (int i = 0; i < buttons.Length; i++) {
			previousColors [i] = buttons [i].image.color;
		}

		while (offset < timeToChangeColor) {
			for (int i = 0; i < buttons.Length; i++) {
				buttons [i].SetColor (Color.Lerp (previousColors [i], theme.Lerp (i / ((float)(buttons.Length - 1)), true), offset / timeToChangeColor));
			}
			yield return null;
			offset += Time.deltaTime;
		}

		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].SetColor (theme.Lerp (i / ((float)(buttons.Length - 1)), true));
		}
	}

	IEnumerator IApplyTheme (Text text, Theme theme) {
		float offset = 0f;
		Color previousColor = text.color;
		while (offset < timeToChangeColor) {
			text.color = Color.Lerp (previousColor, theme.endColor, offset / timeToChangeColor);
			yield return null;
			offset += Time.deltaTime;
		}
		text.color = theme.endColor;
	}

	IEnumerator IApplyTheme (Image image, Theme theme) {
		float offset = 0f;
		Color previousColor = image.color;
		while (offset < timeToChangeColor) {
			image.color = Color.Lerp (previousColor, theme.startColor, offset / timeToChangeColor);
			yield return null;
			offset += Time.deltaTime;
		}
		image.color = theme.startColor;
	}

	void ApplyTheme (MenuButtonWithAnimation1[] buttons, Theme theme) {
		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].SetColor (theme.Lerp (i / ((float)(buttons.Length - 1)), true));
		}
	}

	void Generate () {
		Vector3 position = new Vector3 ();
		position.z = 0f;

		configButtons = new MenuButtonWithAnimation1[CrossDataHandler.mapConfigSet.mapConfigs.Length + 2];
		themeButtons = new MenuButtonWithAnimation1[CrossDataHandler.themeSet.themes.Length];
		playerCountButtons = new MenuButtonWithAnimation1[4];

		int length;
		length = CrossDataHandler.mapConfigSet.mapConfigs.Length;

		for (int b = 0; b < length + 2; b++) {
			Color color = CrossDataHandler.CurrentTheme.Lerp(b / (float)(length + 1), true);
			position.y = (-((length + 2)/ 2f) + b + .5f) * CrossDataHandler.positionConfig.distBetweenRows;

			float xOffset = position.y * Mathf.Tan (CrossDataHandler.positionConfig.angle * Mathf.Deg2Rad);;
			position.x = xOffset;

			MenuButtonWithAnimation1 newButton = Instantiate (buttonPrefab, position, Quaternion.identity) as MenuButtonWithAnimation1;
			newButton.SetColor (color);
			newButton.transform.SetParent (configButtonsHolder);
			int index = length + 1 - b;
			newButton.SetButton (delegate {
				ExecuteButton (Type.MapConfig, index);
			});
			configButtons [b] = newButton;

			if (b > 1) {
				newButton.SetText (CrossDataHandler.mapConfigSet.mapConfigs [length + 1 - b].ToString ());
				if (configButtons.Length - 1 - b == CrossDataHandler.MaxAvailableLevel) {
					newButton.SetInfo (CrossDataHandler.WinTimes + "/" + winRate);
					configButtons [b] = newButton;
					newButton.gameObject.SetActive (false);
				} else {
					if (CrossDataHandler.MaxAvailableLevel < configButtons.Length - 1 - b) {
						newButton.SetIcon (lockSprite);
						newButton.SetButtonInteractable (false);
					} else {
						newButton.SetIcon (doneSprite);
					}
				}
			} else {
				if (b == 0)
					newButton.SetText ("Themes");
				else if (b == 1)
					newButton.SetText ("Tutorial");
			}

			newButton.gameObject.SetActive (false);
		}

		length = CrossDataHandler.themeSet.themes.Length;

		for (int b = 0; b < length; b++) {
			Color color = CrossDataHandler.CurrentTheme.Lerp(b / (float)(length - 1), true);
			position.y = (-((length)/ 2f) + b + .5f) * CrossDataHandler.positionConfig.distBetweenRows;

			float xOffset = position.y * Mathf.Tan (CrossDataHandler.positionConfig.angle * Mathf.Deg2Rad);;
			position.x = xOffset;
			MenuButtonWithAnimation1 newButton = Instantiate (buttonPrefab, position, Quaternion.identity) as MenuButtonWithAnimation1;
			newButton.SetColor (color);
			newButton.transform.SetParent (themeButtonsHolder);
			int index = length - 1 - b;
			newButton.SetButton (delegate {
				ExecuteButton (Type.Theme, index);
			});
			if (length - 1 - b > CrossDataHandler.MaxAvailableThemeIndex) {
				newButton.SetText (CrossDataHandler.themeSet.themes [length - 1 - b].name);
				newButton.SetIcon (lockSprite);
				newButton.SetButtonInteractable (false);
			} else {
				if (length - 1 - b == CrossDataHandler.CurrentThemeIndex) {
					newButton.SetText (CrossDataHandler.themeSet.themes [length - 1 - b].name);
					newButton.SetIcon (doneSprite);
				} else {
					newButton.SetText (CrossDataHandler.themeSet.themes [length - 1 - b].name);
					newButton.ClearInfoAndIcon ();
				}
			}
			themeButtons [b] = newButton;
			newButton.gameObject.SetActive (false);
		}

		for (int b = 0; b < 4; b++) {
			Color color = CrossDataHandler.CurrentTheme.Lerp(b / 3f, true);
			position.y = (-(4f/ 2f) + b + .5f) * CrossDataHandler.positionConfig.distBetweenRows;

			float xOffset = position.y * Mathf.Tan (CrossDataHandler.positionConfig.angle * Mathf.Deg2Rad);;
			position.x = xOffset;
			MenuButtonWithAnimation1 newButton = Instantiate (buttonPrefab, position, Quaternion.identity) as MenuButtonWithAnimation1;
			newButton.SetColor (color);
			newButton.transform.SetParent (playersCountButtonsHolder);
			int index = 4 - b;
			newButton.SetButton (delegate {
				ExecuteButton (Type.Players, index);
			});
			newButton.SetText((b == 3) ? "Computer" : (4 - b).ToString () + " Players");

			playerCountButtons [b] = newButton;
			newButton.gameObject.SetActive (false);
		}

		configButtonsHolder.rotation = Quaternion.Euler (new Vector3 (0, 0, CrossDataHandler.positionConfig.angle));
		themeButtonsHolder.rotation = Quaternion.Euler (new Vector3 (0, 0, CrossDataHandler.positionConfig.angle));
		playersCountButtonsHolder.rotation = Quaternion.Euler (new Vector3 (0, 0, CrossDataHandler.positionConfig.angle));
	}
		
}
