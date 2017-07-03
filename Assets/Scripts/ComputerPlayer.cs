using UnityEngine;
using System.Collections;

public class ComputerPlayer : PlayingEntity {

	public float thinkTime;
	public float betweenStepsTime;
	public float timeBetweenStepsAndGo;

	private int successfulWay;

	public override void StartStep () {
		BaseGameManager.Instance.computersWay = true;
		successfulWay = ClassicStrategy.ShowSuccessfulWay (Map.Instance.CurrentPosition);
		StartCoroutine (IStartStep ());
	}

	public override void EndStep () {return;}

	IEnumerator IStartStep () {
		yield return new WaitForSeconds (thinkTime);
		int rowNum = (successfulWay / 10) - 1;
		int piecesCount = successfulWay % 10;

		while (piecesCount != 0) {
			Piece piece = Map.Instance.rows [rowNum].pieces [Random.Range (0, Map.Instance.rows [rowNum].pieces.Length)];

			if (!piece.takenOut && !piece.active) {
				Go (piece);
				piecesCount--;
				if (piecesCount > 0) yield return new WaitForSeconds (betweenStepsTime);
			}
		}

		yield return new WaitForSeconds (timeBetweenStepsAndGo);
		GoButton.Instance.Go ();

		BaseGameManager.Instance.computersWay = false;
	}

}
