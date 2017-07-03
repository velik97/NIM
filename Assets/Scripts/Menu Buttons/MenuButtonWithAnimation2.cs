using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuButtonWithAnimation2 : MenuButtonWithContent {

	public Animator animator;
	int winTimes;

	public void AddWins () {
		winTimes = CrossDataHandler.WinTimes - 1;
		StartCoroutine (IAddWins ());
	}

	public void SetUnlocked () {
		animator.SetTrigger ("Unlocked");
	}
		

	IEnumerator IAddWins () { 
		if (winTimes == -1)
			winTimes = 2;
		info.text = winTimes + "/3";
		yield return new WaitForSeconds (.7f);
		animator.SetTrigger ("Shake");

		yield return new WaitForSeconds (.1f);

		winTimes++;
		info.text = winTimes + "/3";
		if (winTimes == 3) {
			animator.SetTrigger ("Unlock");
		}
	}

}
