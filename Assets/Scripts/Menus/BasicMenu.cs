using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMenu : MonoBehaviour {

	public MenuButton[] buttons;
	public Animator animator;

	public virtual void SetTheme (Theme theme) {
		buttons [0].SetColor (theme.startColor);
		buttons [buttons.Length - 1].SetColor (theme.endColor);
		for (int i = 1; i < buttons.Length - 1; i++) {
			buttons [i].SetColor (theme.Lerp (((float)i) / ((float)buttons.Length)));
		}
	}

	public virtual void Appear () {
		Activate ();
		animator.SetBool ("Appear", true);
	}

	public void Disappear () {
		animator.SetBool ("Appear", false);
		Invoke ("Disactivate", .5f);
	}

	public void Disactivate () {
		gameObject.SetActive (false);
	}

	public void Activate () {
		gameObject.SetActive (true);
	}
}
