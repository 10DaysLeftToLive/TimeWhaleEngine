using UnityEngine;
using System.Collections;

[AddComponentMenu("ShaderBase")]

public abstract class ShaderBase : PauseObject {
	
	//A variable that is used to interpolate a transparent color to the fade color
	protected float interpolationFactor = 0;
	
	public Shader   shader;
	protected bool shaderNotSupported;
	private Material m_Material;
	
	// Use this for initialization
	void Start () {
		// Disable if we don't support image effects
		if (!SystemInfo.supportsImageEffects) {
			enabled = false;
			shaderNotSupported = true;
			return;
		}
		
		// Disable the image effect if the shader can't
		// run on the users graphics card
		if (!shader || !shader.isSupported) {
			shaderNotSupported = true;
			enabled = false;
			return;
		}

		Initialize();
	}
	
	protected Material material {
		get {
			if (m_Material == null) {
				m_Material = new Material (shader);
				m_Material.hideFlags = HideFlags.HideAndDontSave;
			}
			return m_Material;
		} 
	}
	
	protected virtual void OnDisable() {
		if( m_Material ) {
			DestroyImmediate( m_Material );
		}
	}
	
	
	protected abstract void Initialize();
	
	protected abstract void FadeIn();
	
	// Update is called once per frame
	protected override void UpdateObject () {
		
	}
}
