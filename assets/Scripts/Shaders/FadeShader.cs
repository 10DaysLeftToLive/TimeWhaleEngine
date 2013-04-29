using UnityEngine;
using System.Collections;

using SmoothMoves;

public class FadeShader : ShaderBase {
	//TODO: Optimize the Update Function so that it is not called every tick
	//Solution:? NGUI's UpdateManager
	
	//Drag distance threshold needed to perform a fade
	public int minimumDragDistance;
	
	//Location of the FadePlane if it is not fading
	public Vector2 idlePosition; 
	
	//Denotes the color of the fade
	public Color fadeColor; 
	
	//Duration of the fade in and out in ticks
	public float fadeDuration; 
	
	//The game's camera's position
	public CameraController cameraController;
	
	//A flag that determines if our plane is fading in/out in front of the camera.
	//NOTE:Will get rid of this only temporary until we optimize.
	protected bool isFading = false;

	//Flag that is denotes if the FadePlane should fade in or out.
	private bool fade = true;
	
	private struct FadeShaderConstants {
	
		public const int STOPFADE_THRESHOLD = 2;
		
		public const float FADEPLANEOFFSET = 0.3f;
		
		public const float HIDE_Z_LOC = 1f;
		
	}
	
	/// <summary>
	/// Initialize:
	/// Sets the initial color of the plain to a completely transparent color and
	/// moves the FadePlane offscreen.
	/// </summary>
	protected override void Initialize() {
		renderer.material.color = Color.clear;
		transform.position = new Vector3(idlePosition.x, idlePosition.y, FadeShaderConstants.HIDE_Z_LOC);
		EventManager.instance.mOnDragEvent += new EventManager.mOnDragDelegate (OnDragEvent);
	}
	
	/// <summary>
	/// Raises the drag event event.
	/// </summary>
	/// <param name='EM'>
	/// EventManager that handles all the events
	/// </param>
	/// <param name='dragInformation'>
	/// The distance the finger has moved from the last tick to this tick
	/// </param>
	private void OnDragEvent(EventManager EM, DragArgs dragInformation) {
		Vector2 inputChangeSinceLastTick = dragInformation.dragMagnitude;
		if (inputChangeSinceLastTick.y > 0 &&
			inputChangeSinceLastTick.x == 0 && inputChangeSinceLastTick.magnitude > minimumDragDistance) {
			OnDragUp();
		}
		else if (inputChangeSinceLastTick.y < 0 &&
			inputChangeSinceLastTick.x == 0 && inputChangeSinceLastTick.magnitude > minimumDragDistance) {
			OnDragDown();
		}
	}
	
	/// <summary>
	/// Performs actions when the finger has swiped down.
	/// </summary>
	protected virtual void OnDragDown() {
		
	}
	
	/// <summary>
	/// Peforms actions when the finger has swiped up.
	/// </summary>
	protected virtual void OnDragUp() {
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
			Vector3 cameraPos = cameraController.transform.position;
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
	protected override void FadeIn() 
	{
		renderer.material.color = 
			Color.Lerp(Color.clear, fadeColor, interpolationFactor);
	}
	
	/// <summary>
	/// FadeOut;
	/// Fades out the denoted fade color to a transparent color.
	/// </summary>		
	protected override void FadeOut()
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
	protected virtual void UpdateInterpolationFator() 
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
				OnFadeInComplete();
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
	public virtual void DoFade() 
	{
		if (isFading) {
			renderer.material.color = Color.clear;
		}
		isFading = true;
		fade = true;
		interpolationFactor = 0;
		fadeCycle = 0;
		
		//For optimization later
		//UpdateManager.AddUpdate(this, 0,FadeUpdate); 
	}
	
	/// <summary>
	/// StopFade:
	/// Stops the fade and moves the FadePlane to the idle position.
	/// </summary>
	protected virtual void StopFade()
	{
		isFading = false;
		transform.position = new Vector3(idlePosition.x, idlePosition.y, FadeShaderConstants.HIDE_Z_LOC);
		fadeCycle = 0;
		
		//For optimization later
		//UpdateManager.RemoveUpdate(FadeUpdate);
	}
	
	/// <summary>
	/// Raises the fade in complete event.
	/// </summary>
	public virtual void OnFadeInComplete() {
		
	}

}
