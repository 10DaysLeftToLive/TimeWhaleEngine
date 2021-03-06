using UnityEngine;
using System.Collections;

public class Inventory {
	private GameObject pickedUpObject = null; // Can only hold one item in inventory
	private Transform leftHandTransform;
	private Vector3 originalLocalScale;
	
	public Inventory(Transform rightHand){
		leftHandTransform = rightHand;
	}
	
	public void PickUpObject(GameObject toPickUp){
		if (HasItem()) {
			SwapItems(toPickUp);
		} else {
			originalLocalScale = toPickUp.transform.localScale;
			pickedUpObject = toPickUp;
			
			toPickUp.transform.position = new Vector3(leftHandTransform.position.x, 
													  leftHandTransform.position.y, 
													  leftHandTransform.position.z);
			
			
			toPickUp.transform.parent = leftHandTransform;
			Utils.SetActiveRecursively(leftHandTransform.gameObject, true);
			Utils.SetActiveRecursively(pickedUpObject.gameObject, true);
			
			pickedUpObject.GetComponent<InteractableOnClick>().Disable();
            SoundManager.instance.PlaySFX("PickUp");
		}
		
	}
	
	public bool HasItem() {
		return (pickedUpObject != null);
	}
	
	public GameObject GetItem(){
		return (pickedUpObject);
	}
	
	public void DropItem(Vector3 toPlace) {
		pickedUpObject.GetComponent<InteractableOnClick>().Enable();
		pickedUpObject.transform.parent = null;
		pickedUpObject.transform.localScale = originalLocalScale;
		pickedUpObject.transform.position = toPlace;
		
		pickedUpObject = null;
        SoundManager.instance.PlaySFX("PutDown");
	}
	
	public void SwapItemWithCurrentAge(SmoothMoves.BoneAnimation animationData) {
		if (HasItem()) {
			Vector3 oldScale = pickedUpObject.transform.localScale;
			pickedUpObject.transform.parent = null;
			leftHandTransform = animationData.GetSpriteTransform("Left Hand");
			Utils.SetActiveRecursively(pickedUpObject, true);
			pickedUpObject.transform.position = leftHandTransform.position;
			pickedUpObject.transform.parent = leftHandTransform;
			pickedUpObject.transform.localScale = oldScale;
		}
		else {
		}
	}
	
	public void DisableHeldItem() {
		pickedUpObject.transform.parent = null;
		Utils.SetActiveRecursively(pickedUpObject.gameObject, false);
		pickedUpObject = null;	
	}
	
	public void ChangeRightHand(Transform newRightHand) {
		if (!newRightHand) {
			Debug.LogWarning("Invetory.cs: RIGHT HAND BEING SET TO NULL!");
			return;
		}
		leftHandTransform = newRightHand;
	}
	
	private void SwapItems(GameObject toSwapIn) {
		Vector3 positionToPlace = toSwapIn.transform.position;
		//Vector3 oldScale = pickedUpObject.transform.localScale;
		
		DropItem(positionToPlace);
		//oldPickedUpObject.transform.localScale = oldScale;
		PickUpObject(toSwapIn);
	}
}
