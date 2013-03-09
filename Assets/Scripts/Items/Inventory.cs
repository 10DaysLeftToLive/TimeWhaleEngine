using UnityEngine;
using System.Collections;

public class Inventory {
	private GameObject pickedUpObject = null; // Can only hold one item in inventory
	private Transform rightHandTransform;
	
	public Inventory(Transform rightHand){
		rightHandTransform = rightHand;
	}
	
	public void PickUpObject(GameObject toPickUp){
		if (HasItem()){
			Debug.Log("Swapping");
			SwapItems(toPickUp);
		} else {
			pickedUpObject = toPickUp;
			
			toPickUp.transform.position = new Vector3(rightHandTransform.position.x, 
													  rightHandTransform.position.y, 
													  rightHandTransform.position.z);
			
			toPickUp.transform.parent = rightHandTransform;
			pickedUpObject.GetComponent<InteractableOnClick>().Disable();
		}
		Debug.Log("PickUpObject does " + (HasItem() ? "" : "not ") + "have an item");
		
        SoundManager.instance.PickUpItemSFX.Play();
	}
	
	public bool HasItem(){
		return (pickedUpObject != null);
	}
	
	public GameObject GetItem(){
		return (pickedUpObject);
	}
	
	public void DropItem(Vector3 toPlace) {
		pickedUpObject.GetComponent<InteractableOnClick>().Enable();
		pickedUpObject.transform.parent = null;
		pickedUpObject.transform.position = toPlace;
		pickedUpObject = null;
        SoundManager.instance.PutDownItemSFX.Play();
	}
	
	public void DisableHeldItem(){
		pickedUpObject.SetActiveRecursively(false);
		pickedUpObject = null;	
	}
	
	private void SwapItems(GameObject toSwapIn){
		Vector3 positionToPlace = toSwapIn.transform.position;
		
		DropItem(positionToPlace);
		PickUpObject(toSwapIn);
	}
}
