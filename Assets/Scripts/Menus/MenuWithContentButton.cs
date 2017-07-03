using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWithContentButton : SimpleMenu {

	public MenuButtonWithAnimation2 contentButton;
	public bool unlocked = false;

	public void SetUnlocked () {
		unlocked = true;
	}

	public override void Appear () {
		base.Appear ();
		if (unlocked)
			contentButton.SetUnlocked ();
		else
			contentButton.AddWins ();
	}

	public override void SetTheme (Theme theme) {
		base.SetTheme (theme);
		contentButton.icon.color = theme.Lerp ((1f / ((float)buttons.Length)));
		contentButton.info.color = theme.Lerp ((1f / ((float)buttons.Length)));
	}

}
