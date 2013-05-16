using UnityEngine;
using System.Collections;

using SmoothMoves;

[RequireComponent (typeof(Camera))]
public class FadeEffect : ShaderBase {
	//TODO: Optimize the Update Function so that it is not called every tick
	//Solution:? NGUI's UpdateManager
	
	//Drag distance threshold needed to perform a fade
	public int minimumDragDistance = 5;
	
	//Duration of the fade in and out in ticks
	public float fadeDuration = 5; 
	
	public float frequency = 6;
	
	public float amplitude = 0.4f;
	
	public float angle = 20;
	
	public Vector2 center = new Vector2(0.4f, 0.4f);
	
	public Vector3 fadeInLocation = new Vector3(0f,0f,0f);
	
	//public Camera fadeCamera = null;
	
	public RenderTexture test = null;
	
	//A flag that determines if our plane is fading in/out in front of the camera.
	//NOTE:Will get rid of this only temporary until we optimize.
	public bool isFading = false;
	
	private float _angleDelta = 0; 
	/// <summary>
	/// Initialize:
	/// Sets the initial color of the plain to a completely transparent color and
	/// moves the FadePlane offscreen.
	/// </summary>
	protected override void Initialize() {
		if (fadeDuration <= 0) {
			fadeDuration = 5;
		}
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
	
	void OnRenderImage(RenderTexture source, RenderTexture destination) {
		RenderDistortion (material, source, destination, null, _angleDelta, center);
	}
	
	protected void RenderDistortion(Material material, RenderTexture source, RenderTexture fadeInTexture, RenderTexture destination, float angle, Vector2 center) {
		bool invertY = source.texelSize.y < 0.0f;
        if (invertY)
        {
            center.y = 1.0f - center.y;
            angle = -angle;
        }
		
        material.SetVector("_CenterFrequencyAmplitude", new Vector4(center.x, center.y, frequency, amplitude)); 
        material.SetFloat("_Angle", angle * Mathf.Deg2Rad);
		material.SetFloat("_InterpolationFactor", interpolationFactor);
		material.SetTexture("_FadeInTex", test);
        Graphics.Blit(source, destination, material);
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
			
			//Color fades in and out.
			FadeIn();
			
			//Increments the interpolation factor based on fade speed or resets it to zero.
			UpdateInterpolationFator();
		}

	}
	
	/// <summary>
	/// FadeIn:
	/// Fades in the denoted fade color. 
	/// </summary>
	protected override void FadeIn() 
	{
		_angleDelta += angle;
	}

	/// <summary>
	/// UpdateInterpolationFator:
	/// Increments the interpolation factor based on half the fade speed.  Resets the interpolation
	/// factor to zero when the interpolation factor reaches one which signifys a change from fade
	/// in to fade out.
	/// </summary>
	protected virtual void UpdateInterpolationFator() 
	{
		//Debug.Log ("interpolationFactor: " + interpolationFactor);
		if (interpolationFactor < 1) {
			interpolationFactor += Time.deltaTime / fadeDuration;
		}
		else {
			OnFadeInComplete();
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
		isFading = true;
		interpolationFactor = 0;
		//fadeCamera.transform.position = fadeInLocation;
		//For optimization later
		//UpdateManager.AddUpdate(this, 0,FadeUpdate); 
		EventManager.instance.RiseOnPauseToggleEvent(new PauseStateArgs(true));
	}
	
	/// <summary>
	/// Raises the fade in complete event.
	/// </summary>
	public virtual void OnFadeInComplete() {
		isFading = false;
		EventManager.instance.RiseOnPauseToggleEvent(new PauseStateArgs(false));
	}

}
