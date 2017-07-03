using UnityEngine;
using System.Collections;

public class MenuButtonWithAnimation1 : MenuButtonWithContent {

	public Animator animator;

	public void Appear (bool up) {
		gameObject.SetActive (true);
		animator.SetBool ("Up", up);
		animator.SetBool ("Appear", true);
	}

	public void DisAppear (bool up) {
		animator.SetBool ("Up", up);
		animator.SetBool ("Appear", false);
		Invoke ("DisActivate", .5f);
	}

	void DisActivate () {
		gameObject.SetActive (false);
	}

}
