using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoSingleton <Map> {

	public MapConfig mapConfig;
	public Theme theme;

	public Piece piecePrefab;
	public Row rowPrefab;

	[HideInInspector]
	public bool active;

	[HideInInspector]
	public Row[] rows;

	public virtual void Awake () {
		AssembleConfigs ();
		rows = new Row[mapConfig.rowsLength.Length];
		active = false;

		Generate ();
		TouchManager.Instance.OnMapTap.AddListener (Disactivate);

		StartCoroutine (IAppear ());
	}

	public virtual void AssembleConfigs () {
		theme = CrossDataHandler.CurrentTheme;
		mapConfig = CrossDataHandler.CurrentMapConfig;
	}

	public virtual IEnumerator IAppear () {
		GoButton.Instance.GetComponent <Animator> ().SetBool ("Appear", true);
		for (int r = 0; r < rows.Length; r++) {
			rows [r].Appear ();
			yield return new WaitForSeconds (.1f);
		}
		yield return new WaitForSeconds (.2f);
		Hint.Instance.Appear ();
		BaseGameManager.Instance.StartGame ();
	}

	public void Disappear () {
		StartCoroutine (IDisappear ());	
	}

	public virtual IEnumerator IDisappear () {
		Hint.Instance.Disappear ();
		GoButton.Instance.GetComponent <Animator> ().SetBool ("Appear", false);
		for (int r = rows.Length - 1; r >= 0; r--) {
			rows [r].Disappear ();
			yield return new WaitForSeconds (.1f);
		}
	}

	public void Generate () {
		Vector3 position = new Vector3 ();
		position.z = 0f;

		Vector3 pieceSize = Vector3.one * CrossDataHandler.positionConfig.pieceSize;

		for (int r = 0; r < mapConfig.rowsLength.Length; r++) {
			Color color;
			color = theme.Lerp((mapConfig.rowsLength.Length == 1) ? .5f : r / (float)(mapConfig.rowsLength.Length-1));
			position.y = (-(mapConfig.rowsLength.Length/ 2f) + r + .5f) * CrossDataHandler.positionConfig.distBetweenRows;

			position.x = 0f;
			Row newRow = Instantiate (rowPrefab, position, Quaternion.identity) as Row;
			Color rowColor = color;
			rowColor.a = theme.rowIntence;
			newRow.SetColor (rowColor);
			newRow.transform.localScale = new Vector3 (20f, CrossDataHandler.positionConfig.distBetweenRows, 1f);
			newRow.transform.SetParent (this.transform);
			newRow.pieces = new Piece[mapConfig.rowsLength [r]];
			newRow.pieceColor = color;
			rows [r] = newRow;

			for (int p = 0; p < mapConfig.rowsLength [r]; p++) {
				position.x = (-(mapConfig.rowsLength [r]/ 2f) + p + .5f) * CrossDataHandler.positionConfig.distBetweenPieces;
				Piece newPiece = Instantiate (piecePrefab, position + Vector3.up * 15f, Quaternion.identity) as Piece;
				newPiece.transform.localScale = pieceSize;
				newPiece.SetColor (color);
				newPiece.adultRow = rows [r];
				rows[r].pieces[p] = newPiece;
				newPiece.transform.SetParent (rows[r].sub);
			}

			newRow.transform.position += Vector3.right * position.y * Mathf.Tan (CrossDataHandler.positionConfig.angle * Mathf.Deg2Rad);
//			rows [r].sub.localPosition = Vector3.up * 15f;

		}

		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, CrossDataHandler.positionConfig.angle));

		GoButton.Instance.buttonImage.color = theme.Lerp (0f, true);
		GoButton.Instance.barImage.color = new Color (1, 1, 1, 0);
		GoButton.Instance.colorIntence = theme.rowIntence;
		GoButton.Instance.active = false;
	}

	public void Tap (Row row, Color color) {
		if (!row.active) {
			for (int i = 0; i < rows.Length; i++) {
				if (rows [i] != row) {
					rows [i].DisactivateAll ();
				}
			}
		}
		if (!BaseGameManager.Instance.computersWay) {
			GoButton.Instance.Activate (color);
			active = true;
		}
	}

	public void Disactivate () {
		if (active) {
			for (int i = 0; i < rows.Length; i++) {
				rows [i].DisactivateAll ();
			}

			GoButton.Instance.Disactivate ();
		} else {
			Shake ();
		}

		active = false;
	}

	public void Shake () {
		for (int i = 0; i < rows.Length; i++) {
			rows [i].Shake ();
		}
	}

	public int[] CurrentPosition {
		get {
			int[] position = new int[rows.Length];

			for (int i = 0; i < rows.Length; i++) {
				int piecesCount = 0;
				for (int j = 0; j < rows [i].pieces.Length; j++) {
					if (!rows [i].pieces [j].takenOut) {
						piecesCount++;
					}
				}
				position [i] = piecesCount;
			}

			return position;
		}
	}
		
}
