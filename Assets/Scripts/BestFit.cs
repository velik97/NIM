using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestFit : MonoBehaviour {

	private RectTransform rTransform;
	public float defaultHeight;
	public float defaultWidth;

	public bool check;
	public bool globalFit;

	void Awake () {
		rTransform = GetComponent <RectTransform> ();

		if (check) {
			print (rTransform.rect.height); 
			print (rTransform.rect.width); 
		} else {
			float minChange;
			if (globalFit)
				minChange = Mathf.Min ((float)Screen.height / defaultHeight, (float)Screen.width / defaultWidth);
			else 
				minChange = Mathf.Min (rTransform.rect.height / defaultHeight, rTransform.rect.width / defaultWidth);
			rTransform.localScale = Vector3.one * minChange;
		}
	}
}
