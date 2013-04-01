using UnityEngine;
using System.Collections;

public class ShaderBase : PauseObject {
	
	//Location of the FadePlane if it is not fading
	public Vector2 idlePosition; 
	
	//Denotes the steps sizes for the fade.
	protected int fadeCycle = 0;
	
	//A variable that is used to interpolate a transparent color to the fade color
	protected float interpolationFactor = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
