using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoSingleton <Hint> {

	private List <Piece> shownPieces;

	[Header("Data")]
	public Sprite usualSprite;
	public Sprite hintSprite;
	public float startPitch;
	public float pitchStep;

	[Header("Refs")]
	public AudioSource audioSource;
	public Animator animator;
	public Image image;
	public Text hintsCountText;
	public SimpleMenu takeHintsMenu;
	public SimpleMenu noWayToWinMenu;
	public BasicMenu sideMenu;
	public Header hintsCountHeader;

	public bool HintIsShown {
		get {
			return shownPieces.Count != 0;
		}
		set {
			if (!value) {
				foreach (Piece p in shownPieces) {
					if (!p.takenOut)
						p.Mark (usualSprite);
				}
				shownPieces.Clear ();
			}
		}
	}


	void Awake () {
//		CrossDataHandler.HintsCount = 3;
		Instance.gameObject.SetActive (false);
		hintsCountText.text = CrossDataHandler.HintsCount.ToString ();
		shownPieces = new List<Piece> ();
	}

	public void Appear () {
		gameObject.SetActive (true);
		animator.SetBool ("Appear", true);
	}

	public void SetColor (Color color) {
		image.color = color;
		hintsCountText.color = color;
	}

	public void Disappear () {
		animator.SetBool ("Appear", false);
	}

	public void PreAskHint () {
		if (HintIsShown) {
			foreach (Piece p in shownPieces) {
				p.Shake ();
			}
			audioSource.Play ();
		} else {
			MenuManager.Instance.OpenMenu (sideMenu);
		}
	}

	public void AskHint () {
		bool successfulWayExists;
		int successfulWay = ClassicStrategy.ShowSuccessfulWay (Map.Instance.CurrentPosition, out successfulWayExists);
		if (successfulWayExists) {
			int hintsCount = CrossDataHandler.HintsCount;
			if (hintsCount > 0) {
				hintsCount--;
				hintsCountText.text = hintsCount.ToString ();
				CrossDataHandler.HintsCount = hintsCount;
				StartCoroutine (GiveHint (successfulWay)); 
			} else {
				OpenHintShop ();
			}
		} else {
			Denie ();
		}
	}

	IEnumerator GiveHint (int successfulWay) {
		if (MenuManager.Instance.isInMenu) {
			MenuManager.Instance.CloseTopMenu ();
			yield return new WaitForSeconds (.2f);
		}

		Row row = Map.Instance.rows [(successfulWay / 10) - 1];
		int i = 0;
		int piecesCount = successfulWay % 10;

		float pitch = startPitch;

		while (i < row.pieces.Length && piecesCount > 0) {
			if (!row.pieces [i].takenOut) {
				row.pieces [i].Mark (hintSprite);
				shownPieces.Add (row.pieces[i]);
				piecesCount--;
				audioSource.pitch = pitch;
				audioSource.Play ();
				pitch += pitchStep;
				yield return new WaitForSeconds (.2f);
			}
			i++;
		}

	}



	public void OpenHintShop () {
		BaseMenuManager.Instance.OpenMenu (takeHintsMenu);
		hintsCountHeader.SetText (CrossDataHandler.HintsCount.ToString ());
		hintsCountHeader.Appear ();
	}

	public void LeaveHintShop () {
		if (CrossDataHandler.HintsCount == 0) {
			MenuManager.Instance.CloseAllMenus ();
		} else {
			MenuManager.Instance.CloseTopMenu ();
		}
		hintsCountHeader.DisAppear ();
	}

	void Denie () {
		MenuManager.Instance.OpenMenu (noWayToWinMenu);
	}

}
