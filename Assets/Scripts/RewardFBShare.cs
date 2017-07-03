using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

public class RewardFBShare : Transaction {

	public Text debugText;

	public override void Start () {
		base.Start ();
		if (!FB.IsInitialized) {
			FB.Init ();
		} else {
			FB.ActivateApp ();
		}
	}

	public override void Call () {

		debugText.text = "[FBShare] Tried to call";

		FB.ShareLink (
			contentTitle: "Try to win the puzzle",
			contentURL: new System.Uri("https://play.google.com/store/apps/details?id=com.Nikita_Velikovskiy.pytnashki"),
			contentDescription: "Here's a link to the NIM game",
			callback: OnCall);
	}

	private void OnCall (IShareResult result) {
		
		if (result.Cancelled) {
			
			debugText.text = "[FBShare] Cancaled";
			Denied ("Smth went wrong");
		} else if (!string.IsNullOrEmpty (result.Error)) {

			debugText.text = "[FBShare] Error: " + result.Error;
		} else if (!string.IsNullOrEmpty (result.PostId)) {
			
			debugText.text = "[FBShare] PostID" + result.PostId;
		} else {

			debugText.text = "[FBShare] Succeeded";
			Succeeded ();
		}
	}
}
