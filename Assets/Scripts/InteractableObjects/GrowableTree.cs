using UnityEngine;
using System.Collections;

public class GrowableTree: InteractableObject {
	public GameObject treeMound;
	public TimeSwitchObject tree;
	
	public override void Interact(GameObject toInteractWith){
		if(toInteractWith.name.Equals(Strings.item_treeSeed)){
			treeMound.SetActive(false);
			tree.youngTimeObject.SetActive(true);
			tree.middleTimeObject.SetActive(true);
			tree.oldTimeObject.SetActive(true);
			player.DisableHeldItem();	
		}
	}
}
	