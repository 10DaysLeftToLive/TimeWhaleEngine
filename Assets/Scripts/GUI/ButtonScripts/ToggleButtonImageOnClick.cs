using UnityEngine;
using System.Collections;

public class ToggleButtonImageOnClick : MonoBehaviour {
	public UIAtlas initialImage;
	public UIAtlas toggleImage;
	
	private bool toggleOn = false;
	
	void ToggleImage(){
		if (toggleOn){
			this.GetComponentInChildren<UISlicedSprite>().spriteName = "PauseButton";
		} else {
			this.GetComponentInChildren<UISlicedSprite>().spriteName = "BackButton";
		}
		
		toggleOn = !toggleOn;
	}
}
