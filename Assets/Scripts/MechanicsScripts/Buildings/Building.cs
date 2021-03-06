using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : LinkedObject {
	private bool interiorIsShowing = false;
	public GameObject door;
	public GameObject interior;
	public GameObject exterior;
	private Transform[] interiorObjects;
	private Transform[] exteriorObjects;
	
	public void Start(){
		UpdateObjectArrays();
		Hide(interiorObjects);
	}
	
	private void UpdateObjectArrays(){
		interiorObjects = interior.GetComponentsInChildren<Transform>();
		exteriorObjects = exterior.GetComponentsInChildren<Transform>();
	}
	
	public void ToggleBuilding(){
		BuildingManager.instance.ToggleWithId(id);
	}
	
	public void Toggle(){
		UpdateObjectArrays();
		if (interiorIsShowing){
			Hide (interiorObjects);
			Show (exteriorObjects);
		} else {
			Hide (exteriorObjects);
			Show (interiorObjects);
		}
		interiorIsShowing = !interiorIsShowing;
	}
	
	private void Hide(Transform[] objects){
		foreach (Transform transform in objects){
			if (transform.renderer != null){
				transform.renderer.enabled = false;
			}
			if (transform.CompareTag("Untagged") && transform.collider != null) {
				transform.collider.isTrigger = true;
			} else if (transform.CompareTag("Pushable")){
				transform.rigidbody.useGravity = false;
				transform.collider.isTrigger = true;
			}
		}
	}
	
	private void Show(Transform[] objects){
		foreach (Transform transform in objects){
			if (transform.renderer != null){
				transform.renderer.enabled = true;
			}
			if (transform.CompareTag("Untagged") && transform.collider != null) {
				transform.collider.isTrigger = false;
			} else if (transform.CompareTag("Pushable")){
				transform.rigidbody.useGravity = true;
				transform.collider.isTrigger = false;
			}
		}
	}
}