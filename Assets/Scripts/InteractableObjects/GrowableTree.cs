using UnityEngine;
using System.Collections;

public class GrowableTree: InteractableObject {
	
	public GameObject treeMound;
	public TimeSwitchObject tree;

	
	public override void Interact(GameObject toInteractWith){
		Debug.Log("WE IN HERE " + toInteractWith.name);
		if(toInteractWith.name.Equals(Strings.item_treeSeed)){
			treeMound.active = false;
			tree.youngTimeObject.active = true;
			tree.middleTimeObject.active = true;
			tree.oldTimeObject.active = true;
			Destroy(toInteractWith);
		}
	}
}
