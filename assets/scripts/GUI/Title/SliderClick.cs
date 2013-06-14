using UnityEngine;
using System.Collections;

public class SliderClick : MonoBehaviour {
	
	public UIImageButton imageButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnPress (bool isDown){
		imageButton.target.spriteName = isDown ? imageButton.pressedSprite : imageButton.normalSprite;
		imageButton.target.MakePixelPerfect();	
	}
}
