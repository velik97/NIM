using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(TouchManager))]
public class Player : PlayingEntity {

	public UnityAction tapAction;

	void Awake () {
		tapAction = delegate {
			Go (TouchManager.Instance.closestPiece); 
		};
	}

	public override void StartStep () {
		TouchManager.Instance.OnPieceTap.AddListener (tapAction);
	}

	public override void EndStep () {
		TouchManager.Instance.OnPieceTap.RemoveListener (tapAction);
	}
}
