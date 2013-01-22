using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {
	private bool interiorIsShowing = false;
	public GameObject interior;
	private Transform[] interiorObjects;
	
	public void Start(){
		interiorObjects = interior.GetComponentsInChildren<Transform>();
		HideInterior();
	}
	
	public void ToggleHouse(){
		Debug.Log("Toggle House interiorIsShowing = " + interiorIsShowing);
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
			transform.collider.isTrigger = true;
		}
		this.transform.renderer.enabled = true;
	}
	
	private void ShowInterior(){
		foreach (Transform transform in interiorObjects){
			transform.renderer.enabled = true;
			transform.collider.isTrigger = false;
		}
		this.transform.renderer.enabled = false;
	}
}