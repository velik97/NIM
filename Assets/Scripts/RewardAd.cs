using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardAd : Transaction {

	public override void Start () {
		base.Start ();
		Advertisement.Initialize ("1292139");
	}
	
	public override void Call () {
		if (Advertisement.IsReady ()) {
			Advertisement.Show (new ShowOptions {
				resultCallback = resault => {
					if (resault == ShowResult.Finished)
						Succeeded ();
					else if (resault == ShowResult.Failed)
						Denied ("Smth went wrong");
					else if (resault == ShowResult.Skipped)
						Denied ("Don't skip ad!");
				}
			});
		} else {
			Denied ("Smth went wrong");
		}
	}
}
