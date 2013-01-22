using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class House : MonoBehaviour {
	private bool interiorIsShowing = false;
	public GameObject door;
	public GameObject interior;
	public GameObject exterior;
	private Transform[] interiorObjects;
	private Transform[] exteriorObjects;
	
	public void Start(){
		interiorObjects = interior.GetComponentsInChildren<Transform>();
		exteriorObjects = exterior.GetComponentsInChildren<Transform>();
		Hide(interiorObjects);
	}
	
	public void ToggleHouse(){
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
			transform.renderer.enabled = false;
			if (transform.CompareTag("Untagged") && transform.collider != null) {
				transform.collider.isTrigger = true;
			}
		}
	}
	
	private void Show(Transform[] objects){
		foreach (Transform transform in objects){
			transform.renderer.enabled = true;
			if (transform.CompareTag("Untagged") && transform.collider != null) {
				transform.collider.isTrigger = false;
			}
		}
	}
}