using UnityEngine;
using SmoothMoves;
using System.Collections;

public class StepOn : MonoBehaviour {
	bool wasStepedOn = false;
	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.name == Strings.Player && !wasStepedOn) {
			this.GetComponent<SmoothMoves.BoneAnimation>().Play("Explode");
			FlagManager.instance.SetFlag(FlagStrings.CrushFrog);
			wasStepedOn = true;
		}
	}
}
