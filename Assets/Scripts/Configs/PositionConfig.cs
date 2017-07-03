using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PositionConfig", menuName = "Configs/Position Config", order = 2)]
public class PositionConfig : ScriptableObject {

	[Range(-90,0)]
	public float angle;

	[Space(10)]
	public float pieceSize;
	public float distBetweenPieces; // in single row
	public float distBetweenRows;
	public float gapBetweenRows;

	public PositionConfig (float angle, float pieceSize, float distBetweenPieces, float distBetweenRows, float gapBetweenRows) {
		this.angle = angle;
		this.pieceSize = pieceSize;
		this.distBetweenPieces = distBetweenPieces;
		this.distBetweenRows = distBetweenRows;
		this.gapBetweenRows = gapBetweenRows;
	}

}

