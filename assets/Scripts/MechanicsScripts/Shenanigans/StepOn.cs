using UnityEngine;
using SmoothMoves;
using System.Collections;

public class StepOn : MonoBehaviour {
	void OnTriggerEnter(Collider collider) {
		Debug.Log("FROGIAMDEAD!");
		if (collider.gameObject.name == Strings.Player) {
			this.GetComponent<SmoothMoves.BoneAnimation>().Play("Explode");
			//this.GetComponent<SmoothMoves.Sprite>().atlas.material = squashed;
		}
	}
}
