using UnityEngine;
using System.Collections;
using SmoothMoves;

public class AgeTransitionShader : MonoBehaviour {
	//TODO: Optimize the Update Function so that it is not called every tick
	//Solution:? NGUI's UpdateManager
	
	//Denotes the color of the fade
	public Color fadeColor; 
	
	//Duration of the fade in and out in ticks
	public float fadeDuration; 
	
	//Location of the FadePlane if it is not fading
	public Vector2 idlePosition; 
	
	//Script for the camera
	private CameraController camera;
	
	//A flag that determines if our plane is fading in/out in front of the camera.
	//NOTE:Will get rid of this only temporary until we optimize.
	protected bool isFading = false;

	//Flag that is denotes if the FadePlane should fade in or out.
	private bool fade = true;
	
	private struct FadeShaderConstants {
	
		public const int STOPFADE_THRESHOLD = 2;
		
		public const float FADEPLANEOFFSET = 0.3f;
		
		public const float HIDE_Z_LOC = -1f;
	}
	
	//Denotes the steps sizes for the fade.
	private int fadeCycle = 0;
	
	//A variable that is used to interpolate a transparent color to the fade color
	private float interpolationFactor = 0;
	
	//Will get rid of this if Jared lets me refactor LevelManager a tiny bit.
	private LevelManager levelManager;
	
	//Button pressed that we used to activate the fade
	private string ageShiftAction;
	
	/// <summary>
	/// Start:
	/// Sets the initial color of the plain to a completely transparent color and
	/// moves the FadePlane offscreen.
	/// </summary>
	void Start () {
		renderer.material.color = Color.clear;
		camera = FindObjectOfType(typeof(CameraController)) as CameraController;
		transform.position = new Vector3(idlePosition.x, idlePosition.y, FadeShaderConstants.FADEPLANEOFFSET);
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}

	/// <summary>
	/// Update:
	/// Does nothing unless the FadePlane needs to fade in or out.
	/// Fades the FadePlane in and out while interpolating the colors from a transparent
	/// color to the fadeColor based on half the fade speed.
	/// </summary>
	void Update () {
		if (isFading) {
			//Moves the FadePlane to the front of the screen.
			Vector3 cameraPos = camera.transform.position;
			transform.position = new Vector3(cameraPos.x, cameraPos.y, cameraPos.z + FadeShaderConstants.FADEPLANEOFFSET);
			
			//Color fades in and out.
			if (fade) {
				FadeIn();
			}
			else {
				FadeOut();
			}
			
			//Increments the interpolation factor based on fade speed or resets it to zero.
			UpdateInterpolationFator();
			
			//Stops the fade when the FadePlane has faded in and out once.
			if (fadeCycle == FadeShaderConstants.STOPFADE_THRESHOLD) 
			{
				StopFade();
			}
		}
	}
	
	/// <summary>
	/// FadeIn:
	/// Fades in the denoted fade color. 
	/// </summary>
	protected void FadeIn() 
	{
		renderer.material.color = 
			Color.Lerp(Color.clear, fadeColor, interpolationFactor);
	}
	
	/// <summary>
	/// FadeOut;
	/// Fades out the denoted fade color to a transparent color.
	/// </summary>		
	protected void FadeOut()
	{
		renderer.material.color = 
			Color.Lerp(fadeColor, Color.clear, interpolationFactor);
	}
	
	
	/// <summary>
	/// UpdateInterpolationFator:
	/// Increments the interpolation factor based on half the fade speed.  Resets the interpolation
	/// factor to zero when the interpolation factor reaches one which signifys a change from fade
	/// in to fade out.
	/// </summary>
	protected void UpdateInterpolationFator() 
	{
		if (interpolationFactor < 1) {
			interpolationFactor += Time.deltaTime / (fadeDuration/2);
		}
		else {
			interpolationFactor = 0;
			//Pauses the game until the fade is done.
			EventManager.instance.RiseOnPauseToggleEvent(new PauseStateArgs(fade));
			
			//Switch fade in to fade out.
			fade = fade ? false : true;
			fadeCycle++;
			
			//We shift the age after the fade in is done.
			//This way the change in age is completely hidden by the fade.
			if (fadeCycle == 1) {
				ShiftAge();
			}
		}
	}
	
	/// <summary>
	/// DoFade:
	/// Resets the variables for the FadePlane to fade in.  If the
	/// FadePlane is fading in out then this function resets the color
	/// of the FadePlane to transparent.
	/// </summary>
	/// <param name='ageShiftAction'>
	/// The age shift action used to make the FadePlane fade in and out.
	/// </param>
	public void DoFade(string ageShiftAction) 
	{
		if (isFading) {
			renderer.material.color = Color.clear;
		}
		isFading = true;
		fade = true;
		interpolationFactor = 0;
		fadeCycle = 0;
		this.ageShiftAction = ageShiftAction;
		
		//For optimization later
		//UpdateManager.AddUpdate(this, 0,FadeUpdate); 
	}
	
	/// <summary>
	/// StopFade:
	/// Stops the fade and moves the FadePlane to the idle position.
	/// </summary>
	protected void StopFade()
	{
		isFading = false;
		transform.position = new Vector3(idlePosition.x, idlePosition.y, FadeShaderConstants.HIDE_Z_LOC);
		fadeCycle = 0;
		
		//For optimization later
		//UpdateManager.RemoveUpdate(FadeUpdate);
	}
	
	/// <summary>
	/// ShiftAge:
	/// Shifts the player character's age up or down based on 
	/// what the player used to trigger the fade.
	/// </summary>
	protected void ShiftAge() {
		if (ageShiftAction.Equals(Strings.ButtonAgeShiftDown)) {
			levelManager.ShiftDownAge();
		}
		else {
			levelManager.ShiftUpAge();
		}
	}
	
	
}
	