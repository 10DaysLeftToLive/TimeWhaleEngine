using UnityEngine;
using System;
using System.Collections;

using SmoothMoves;

[RequireComponent (typeof(Camera))]
public class FadeEffect : ShaderBase {
	
	//Drag distance threshold needed to perform a fade
	public int minimumDragDistance = 5;
	
	//Duration of the fade in and out in ticks
	public float fadeDuration = 5; 
	
	public Color fadeColor = new Color();
	
	#region FadePlaneEffects
	
	public GameObject fadePlane = null;
	
	protected bool fade = false;
	
	public static class FADEPLANECONSTANTS {
		public const float planeToCameraOffset = 0.31f;
		
	}
	
	private Vector3 idlePosition = new Vector3(5000,5000,-2);
	
	#endregion
	
	#region SwirlShaderEffects
	public bool isFading = false;
	
	private bool wasFading = false;
	
	public float frequency = 6;
	
	public float amplitude = 0.4f;
	
	public float angle = 20;
	
	private float _angleDelta = 0; 
	
	public Vector2 center = new Vector2(0.5f, 0.5f);
	
	
	#endregion
	
	private Texture2D transitionTexture;
	
	private Rect renderQuad;
	
	/// <summary>
	/// Initialize:
	/// Sets the initial color of the plain to a completely transparent color and
	/// moves the FadePlane offscreen.
	/// </summary>
	protected override void Initialize() {
		if (fadeDuration <= 0) {
			fadeDuration = 5;
		}
		
		if (!shaderNotSupported) {
			transitionTexture = new Texture2D(256, 256);
			renderQuad = new Rect(0, 0, 256, 256);
			Destroy(fadePlane);
		}
		else {
			if (fadePlane == null) {
				Debug.LogError("Please attach the fade plane to this script, thank you.");
			}
			fadePlane.transform.position = idlePosition;
			fadePlane.renderer.material.color = Color.clear;
			fadeDuration /= 2;
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
		if (!isFading) {
			DoFade();
		}
	}
	
	/// <summary>
	/// Peforms actions when the finger has swiped up.
	/// </summary>
	protected virtual void OnDragUp() {
		if (!isFading) {
			DoFade();
		}
	}
	
	void OnRenderImage(RenderTexture source, RenderTexture destination) {
		RenderDistortion (material, source, destination);
	}
	
	void Update() {
		if (shaderNotSupported) {
			RenderFadePlane();
		}
	}
	
	protected void RenderDistortion(Material material, RenderTexture source, RenderTexture destination) {
		
		bool invertY = source.texelSize.y < 0.0f;
        if (invertY)
        {
            angle = -angle;
        }
		
		if (isFading) {
			if (!wasFading && isFading) {
				OnFadeStart(source);
			}
        	FadeIn();
			material.SetTexture("_FadeInTex", source);
			_angleDelta += angle;
			UpdateInterpolationFactorForShader();
			Graphics.Blit(transitionTexture, destination, material);
		}
		else {
			material.SetVector("_CenterFrequencyAmplitude", new Vector4(0f, 0f, 0f, 0f));
			material.SetFloat("_Angle", 0f);
			material.SetFloat("_InterpolationFactor", 0f);
			material.SetTexture("_FadeInTex", source);
			Graphics.Blit(source, destination, material);
		}
		wasFading = isFading;
	}

	protected void RenderFadePlane() {
		if (isFading) {
			fadePlane.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y  + FADEPLANECONSTANTS.planeToCameraOffset, 
				camera.transform.position.z + FADEPLANECONSTANTS.planeToCameraOffset);
			if (fade) {
				FadeIn();
			}
			else {
				FadeOut();
			}
			UpdateInterpolationFactorForFadePlane();
		}
	}
	#region FadeStates
	protected virtual void OnFadeStart(RenderTexture source) {
		if (transitionTexture.width != source.width || transitionTexture.height != source.height) {
			transitionTexture.Resize(source.width, source.height);
			renderQuad.Set(0, 0, source.width, source.height);
		}
		RenderTexture.active = source;
		transitionTexture.ReadPixels(renderQuad, 0, 0);
		transitionTexture.Apply();
	}
	
	/// <summary>
	/// FadeIn:
	/// Fades in the denoted fade color. 
	/// </summary>
	protected override void FadeIn() 
	{
		if (shaderNotSupported) {
			fadePlane.renderer.material.color = Color.Lerp(Color.clear, fadeColor, interpolationFactor);
		}
		else {
			material.SetVector("_CenterFrequencyAmplitude", new Vector4(center.x, center.y, frequency, amplitude)); 
			material.SetFloat("_Angle", Mathf.Deg2Rad * _angleDelta);
			material.SetFloat("_InterpolationFactor", interpolationFactor);
		}
	}
	
	protected virtual void FadeOut() {
		fadePlane.renderer.material.color = Color.Lerp(fadeColor, Color.clear, interpolationFactor);
		if (fadePlane.renderer.material.color.Equals(Color.clear)) {
			OnFadeInComplete();
		}
	}
	#endregion
	
	#region UpdatesInterpolationFactor
	/// <summary>
	/// UpdateInterpolationFator:
	/// Increments the interpolation factor based on half the fade speed.  Resets the interpolation
	/// factor to zero when the interpolation factor reaches one which signifys a change from fade
	/// in to fade out.
	/// </summary>
	protected virtual void UpdateInterpolationFactor() 
	{
		if (shaderNotSupported) {
			UpdateInterpolationFactorForFadePlane();
		}
		else {
			UpdateInterpolationFactorForShader ();
		}
	}
	
	
	
	protected virtual void UpdateInterpolationFactorForShader ()
	{
		if (interpolationFactor < 1) {
			interpolationFactor += Time.deltaTime / fadeDuration;
		}
		else {
			OnFadeInComplete();
		}
	}
	
	protected virtual void UpdateInterpolationFactorForFadePlane() {
		if (interpolationFactor < 1) {
			interpolationFactor += Time.deltaTime / (fadeDuration / 2);
		}
		else if (fade) {
			interpolationFactor = 0;
			fade = false;
		}
		else {
			OnFadeInComplete();
		}
		
		
	}
	#endregion
	
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
		if (shaderNotSupported) {
			fade = true;
		}
		EventManager.instance.RiseOnPauseToggleEvent(new PauseStateArgs(true));
	}
	
	/// <summary>
	/// Raises the fade in complete event.
	/// </summary>
	public virtual void OnFadeInComplete() {
		isFading = false;
		if (shaderNotSupported) {
			fadePlane.transform.position = idlePosition;
		}
		EventManager.instance.RiseOnPauseToggleEvent(new PauseStateArgs(false));
	}

}
