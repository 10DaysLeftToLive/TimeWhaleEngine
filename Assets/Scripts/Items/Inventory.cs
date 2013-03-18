using UnityEngine;
using System.Collections;

public class Inventory {
	private GameObject pickedUpObject = null; // Can only hold one item in inventory
	private Transform rightHandTransform;
	private Vector3 originalLocalScale;
	
	public Inventory(Transform rightHand){
		rightHandTransform = rightHand;
	}
	
	public void PickUpObject(GameObject toPickUp){
		if (HasItem()){
			Debug.Log("Swapping");
			SwapItems(toPickUp);
		} else {
			pickedUpObject = toPickUp;
			
			Debug.Log ("Before: Item in hand: " + pickedUpObject.transform.localScale);
			
			//originalLocalScale = toPickUp.transform.localScale;
			toPickUp.transform.position = new Vector3(rightHandTransform.position.x, 
													  rightHandTransform.position.y, 
													  rightHandTransform.position.z);
			
			toPickUp.transform.parent = rightHandTransform;

			Debug.Log ("After(" + pickedUpObject.name + "): Item in hand: " + pickedUpObject.transform.localScale);
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
		Debug.Log ("Before: Item to swap: " + pickedUpObject.transform.localScale);
		GameObject oldPickedUpObject = pickedUpObject;
		pickedUpObject.GetComponent<InteractableOnClick>().Enable();
		pickedUpObject.transform.parent = null;
		pickedUpObject.transform.position = toPlace;
		//pickedUpObject.transform.localScale = originalLocalScale;
		pickedUpObject = null;
        SoundManager.instance.PutDownItemSFX.Play();
		Debug.Log ("After(" + oldPickedUpObject.name + "): Item to swap: " + oldPickedUpObject.transform.localScale);
	}
	
	public void DisableHeldItem(){
		pickedUpObject.SetActiveRecursively(false);
		pickedUpObject = null;	
	}
	
	private void SwapItems(GameObject toSwapIn) {
		
		Vector3 positionToPlace = toSwapIn.transform.position;
		//Vector3 oldScale = pickedUpObject.transform.localScale;
		
		DropItem(positionToPlace);
		//oldPickedUpObject.transform.localScale = oldScale;
		PickUpObject(toSwapIn);
		
	}
}
