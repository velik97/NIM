using UnityEngine;
using System.Collections;

[System.Serializable]
public class Theme {

	public string name;

	public Color startColor;
	public Color endColor;
	[Range(0f, 1f)]
	public float rowIntence;

	public Theme (string name, Color startColor, Color endColor, float rowIntence) {
		this.name = name;
		this.startColor = startColor;
		this.endColor = endColor;
		this.rowIntence = rowIntence;
	}

	public Theme (Color startColor, Color endColor, float rowIntence) {
		this.name = "";
		this.startColor = startColor;
		this.endColor = endColor;
		this.rowIntence = rowIntence;
	}

	public Theme (Color startColor, Color endColor) {
		this.name = "";
		this.startColor = startColor;
		this.endColor = endColor;
		this.rowIntence = 1f;
	}

	public static Theme ThemeLerp (Theme a, Theme b, float lerp) {
		return new Theme (Color.Lerp (a.startColor, b.startColor, lerp), Color.Lerp (a.endColor, b.endColor, lerp), Mathf.Lerp (a.rowIntence, b.rowIntence, lerp));
	}

	public Color Lerp (float lerp) {
		return Color.Lerp (startColor, endColor, lerp);
	}

	public Color Lerp (float lerp, bool isSubColor) {
		Color color = Lerp (lerp);
		if (isSubColor) {
			color.a = rowIntence;
		}
		return color;
	}

}
