using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleMenu : BasicMenu {

	public Image top;

	public override void SetTheme (Theme theme) {
		top.color = theme.startColor;
		base.SetTheme (theme);
	}
}
