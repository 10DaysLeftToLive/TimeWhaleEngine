using UnityEngine;
using System.Collections;

public class GrowableTree: InteractableObject {
	
	public GameObject treeMound;
	public TimeSwitchObject tree;

	
	public override void Interact(GameObject toInteractWith){
		Debug.Log("WE IN HERE " + toInteractWith.name);
		if(toInteractWith.name.Equals(Strings.item_treeSeed)){
			treeMound.SetActiveRecursively(false);
			tree.youngTimeObject.SetActiveRecursively(true);
			tree.middleTimeObject.SetActiveRecursively(true);
			tree.oldTimeObject.SetActiveRecursively(true);
			playerCharacter.DisableHeldItem();
			
		}
	}
}
	