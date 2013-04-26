using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	public GameObject arrow;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnSliderChange(float val){
		if(val > 0){
			 EnableArrow();
		}else{
			DisableArrow();
		}
	}
	
	public void EnableArrow(){
		Utils.SetActiveRecursively(arrow, true);
	}
	
	public void DisableArrow(){
		Utils.SetActiveRecursively(arrow, false);
	}
}
