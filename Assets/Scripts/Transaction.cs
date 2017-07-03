using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Transaction : MonoBehaviour {

	public int reward;
	public Text hintsCountText;

	public virtual void Start () {
		hintsCountText.text = reward.ToString ();
	}

	public abstract void Call ();

	protected void Succeeded () {
		TransactionHandler.Instance.Succeeded (reward);
	}

	protected void Denied (string reason) {
		TransactionHandler.Instance.Denied (reason);
	}

}
