using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuButtonWithContent : MenuButton {

	public Text text;
	public Text info;
	public Image icon;

	public Button button;

	public void SetText (string text) {
		this.text.text = text;
	}

	public void SetInfo (string info) {
		this.info.text = info;
		icon.gameObject.SetActive (false);
	}

	public void SetIcon (Sprite icon) {
		this.icon.gameObject.SetActive (true);
		this.icon.sprite = icon;
		this.info.text = "";
	}

	public void ClearInfoAndIcon () {
		info.text = "";
		icon.gameObject.SetActive (false);
	}

	public void SetButtonInteractable (bool interactable) {
		button.interactable = interactable;
	}

	public void SetButton (UnityAction listener) {
		button.onClick.AddListener (listener);
	}

}
