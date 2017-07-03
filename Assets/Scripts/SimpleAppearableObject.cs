using UnityEngine;
using System.Collections;

public class SimpleAppearableObject : MonoBehaviour {

	public Animator animator;

	public void Appear () {
		animator.SetBool ("Appear", true);
	}
	public void DisAppear () {
		animator.SetBool ("Appear", false);
	}

}
