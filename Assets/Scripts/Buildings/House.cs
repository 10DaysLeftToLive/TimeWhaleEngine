using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class House : MonoBehaviour {
	private bool interiorIsShowing = false;
	public GameObject interior;
	public GameObject door;
	private Transform[] interiorObjects;
	
	public void Start(){
		interiorObjects = interior.GetComponentsInChildren<Transform>();
		HideInterior();
	}
	
	public void ToggleHouse(){
		if (interiorIsShowing){
			HideInterior();
		} else {
			ShowInterior();
		}
		interiorIsShowing = !interiorIsShowing;
	}
	
	private void HideInterior(){
		foreach (Transform transform in interiorObjects){
			transform.renderer.enabled = false;
			if (transform.CompareTag("Untagged")) {
				transform.collider.isTrigger = true;
			}
		}
		this.transform.renderer.enabled = true;	
	}
	
	private void ShowInterior(){
		foreach (Transform transform in interiorObjects){
			transform.renderer.enabled = true;
			if (transform.CompareTag("Untagged")) {
				transform.collider.isTrigger = false;
			}
		}
		this.transform.renderer.enabled = false;
	}
}