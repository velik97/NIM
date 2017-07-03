using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransactionHandler : MonoSingleton <TransactionHandler> {

	public SimpleMenu resaultMenu;
	public Header hintsCountHeader;

	public GameObject alertTextObject;
	public GameObject hintsTextOblect;

	public bool succeeded;

	public void Succeeded (int reward) {
		CrossDataHandler.HintsCount += reward;
		Hint.Instance.hintsCountText.text = CrossDataHandler.HintsCount.ToString ();
		alertTextObject.SetActive (false);
		hintsTextOblect.SetActive (true);
		hintsTextOblect.GetComponentInChildren <Text> ().text = "+" + reward.ToString ();
		MenuManager.Instance.OpenMenu (resaultMenu);
		succeeded = true;
	}

	public void Denied (string reason) {
		alertTextObject.SetActive (true);
		hintsTextOblect.SetActive (false);
		alertTextObject.GetComponentInChildren <Text> ().text = reason;
		MenuManager.Instance.OpenMenu (resaultMenu);
		succeeded = false;
	}

	public void CloseResaultMenu () {
		MenuManager.Instance.CloseTopMenu ();
		if (succeeded)
			Invoke ("AddHints", .5f);
	}

	public void AddHints () {
		hintsCountHeader.ReplaceText (CrossDataHandler.HintsCount.ToString ());
	}

}
