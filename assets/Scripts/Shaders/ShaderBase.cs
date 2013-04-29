using UnityEngine;
using System.Collections;

public abstract class ShaderBase : PauseObject {
	
	//Denotes the steps sizes for the fade.
	protected int fadeCycle = 0;
	
	//A variable that is used to interpolate a transparent color to the fade color
	protected float interpolationFactor = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	protected abstract void Initialize();
	protected abstract void FadeIn();
	protected abstract void FadeOut();
	
	// Update is called once per frame
	protected override void UpdateObject () {
		
	}
}
