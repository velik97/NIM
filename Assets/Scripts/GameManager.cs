using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : BaseGameManager {

	private int playersCount;

	public override void StartGame() {
		playersCount = CrossDataHandler.PlayersCount;

		if (playersCount > 1) {
			goingPlayer = 0;
			gameHeader.Appear ();
			gameHeader.SetText ("Player " + (goingPlayer + 1));
			gameHeader.SetColor (Map.Instance.theme.Lerp (((float)goingPlayer) / ((float)(playersCount - 1))));
			player.StartStep ();
		} else {
			goingPlayer = 0;
			gameHeader.Appear ();
			gameHeader.SetText ("Player");
			gameHeader.SetColor (Map.Instance.theme.Lerp (0f));
			if (goingPlayer == 0) {
				player.StartStep ();
			} else {
				computerPlayer.StartStep ();
			}
		}
	}
		

	public override void NextGo() {
		if (playersCount > 1) {
			goingPlayer = (goingPlayer + 1) % playersCount;
			gameHeader.ReplaceText ("Player " + (goingPlayer + 1), Map.Instance.theme.Lerp (((float)goingPlayer) / ((float)(playersCount - 1))));
		} else {
			goingPlayer = (goingPlayer + 1) % 2;
			gameHeader.ReplaceText (goingPlayer == 0 ? "Player" : "Computer", Map.Instance.theme.Lerp ((float)goingPlayer));

			if (goingPlayer == 0) {
				player.StartStep ();
			} else {
				player.EndStep ();
				computerPlayer.StartStep ();
			}
		}
	}

	public override void GameOver() {
		gameHeader.ReplaceText ("Game Over");
		if (playersCount == 1) {
			BaseMenuManager.Instance.GameOver (goingPlayer == 0);
		} else {
			BaseMenuManager.Instance.GameOver (goingPlayer + 1);
		}
	}
		
}
