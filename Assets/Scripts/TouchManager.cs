using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TouchManager : MonoSingleton <TouchManager> {

	private LayerMask touchFieldsLayer;
	public float maxDistToPiece;
	private bool isTouching;
	private Vector3 hitPoint;
	[HideInInspector] public Piece closestPiece;

	public UnityEvent OnPieceTap;
	public UnityEvent OnMapTap;

	void Start () {
		Initialize ();
	}
	
	void Update () {
		if ((Input.touches.Length > 0 || Input.GetMouseButton (0)) && !BaseMenuManager.Instance.isInMenu) {
			Vector3 inputPosition;

#if UNITY_EDITOR
			inputPosition = Input.mousePosition;
#else
			inputPosition = (Vector3)Input.touches[0].position;
#endif

			Ray camRay = Camera.main.ScreenPointToRay (inputPosition);
			RaycastHit hit;

			if (Physics.Raycast (camRay, out hit, 20f, touchFieldsLayer)) {
				if (!isTouching) {
					hitPoint = hit.point;
					Touch ();
				}
				isTouching = true;
			}
		} else {
			isTouching = false;
		}
	}

	void Touch () {
		closestPiece = Map.Instance.rows [0].pieces [0];
		float sqrDistToClosestPiece = Vector3.SqrMagnitude (closestPiece.transform.position - hitPoint);

		foreach (Row row in Map.Instance.rows) {
			foreach (Piece piece in row.pieces) {
				float sqrDist = Vector3.SqrMagnitude (piece.transform.position - hitPoint);
				if (sqrDist < sqrDistToClosestPiece) {
					closestPiece = piece;
					sqrDistToClosestPiece = sqrDist;
				}
			}
		}

		if (sqrDistToClosestPiece < maxDistToPiece * maxDistToPiece) {
			OnPieceTap.Invoke ();
		} else {
			OnMapTap.Invoke ();
		}
	}



	void Initialize() {
		touchFieldsLayer = LayerMask.GetMask ("Touch Field");
		isTouching = false;
	}
		
}
