using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseGameManager : MonoSingleton <BaseGameManager> {

	public int goingPlayer;

	public Player player;
	public ComputerPlayer computerPlayer;

	public bool computersWay;

	public bool isInMenu;

	public Header gameHeader;

	public virtual void StartGame () {
		isInMenu = false;
		goingPlayer = 0;
		gameHeader.SetText ("Player");
		gameHeader.SetColor (Map.Instance.theme.Lerp (0f));
		gameHeader.Appear ();
		if (goingPlayer == 0) {
			player.StartStep ();
		} else {
			computerPlayer.StartStep ();
		}
	}

	public virtual void NextGo() {
		goingPlayer = (goingPlayer + 1) % 2;
		gameHeader.ReplaceText (goingPlayer == 0 ? "Player" : "Computer", Map.Instance.theme.Lerp ((float)goingPlayer));

		if (goingPlayer == 0) {
			player.StartStep ();
		} else {
			player.EndStep ();
			computerPlayer.StartStep ();
		}
	}

	public virtual void GameOver() {
		BaseMenuManager.Instance.GameOver (goingPlayer == 0);
	}

}
