using UnityEngine;
using System.Collections;

//This class will not work if the game has to pause!
//STILL WORKING ON THIS!!! NO TOUCHY!!!
public class SunriseSunsetTimer : ShaderBase {
	
	private Color[] vertexColors;
	
	public float sunriseStartTime;
	
	public float sunsetStartTime;
	
	public float sunriseDuration;
	
	public float sunsetDuration;
	
	public float maxBrightness;
	
	public float minBrightness;
	
	private float sunriseEndTime;
	
	private float sunsetEndTime;
	
	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		vertexColors = new Color[mesh.vertices.Length];
//		Debug.Log("Vertex count: " + mesh.vertices.Length);
//		
//		int colorLayerThreshold = (int)Mathf.Floor(mesh.vertices.Length / 3);
//		
		
		//Known holes: 29-31
		//			   60-62
		//			   74-76
		//
		
		//Color layer levels:
		//55
		//68
		
		Color sunsetRed = new Color(150, 65, 33, 1);
		
		SetVertexColors(sunsetRed, 0, 29);
		SetVertexColors(sunsetRed, 31, 35);
//		//35 is the treshold!!!
//		SetVertexColors(Color.blue, 35, 55);
//		SetVertexColors(Color.blue, 29, 31);
		
//		SetVertexColors(Color.green, 55, 60);
//		SetVertexColors(Color.green, 62, 74);
//		SetVertexColors(Color.green, 76, 85);
		//SetVertexColors(Color.green, 33, 60);
		//SetVertexColors(Color.blue, 60, mesh.vertices.Length);
		
		sunriseStartTime += Time.time;
		sunsetStartTime += Time.time;
		sunriseEndTime = sunriseStartTime + sunriseDuration;
		sunsetEndTime = sunsetStartTime + sunsetDuration; 
		
		mesh.colors = vertexColors;
	}
	
	// Update is called once per frame
	protected override void UpdateObject () {
		float currentTime = Time.time;
		if (currentTime > sunriseStartTime && currentTime < sunriseEndTime) {
			FadeOut();
		}
		else if (currentTime > sunsetStartTime && currentTime < sunsetEndTime) {
			FadeIn();
		}
	}
	
	
	
	//Insert mathematical function for sunrise/sunset here.
	protected virtual float ChangeHue(bool isSunrise) {
		float currentTime = isSunrise ? Time.time - sunriseStartTime : sunsetStartTime - Time.time;
		
		int k = 5;
		int g = -170;
		
		float fx = g - g*Mathf.Cos(k*currentTime) - ((2*Mathf.PI)/k)/2;
		fx =  isSunrise ? fx : fx + -170;
		return fx * 180/Mathf.PI;
	}
	
	protected virtual float ChangeBrightness(bool isSunrise) {
		float currentTime;
		if (isSunrise) {
			currentTime = (Time.time - sunriseStartTime) / sunriseDuration;
			return Mathf.Lerp(minBrightness, maxBrightness, currentTime);
		}
		else {
			currentTime = (Time.time - sunsetStartTime) / sunsetDuration;
			return Mathf.Lerp(maxBrightness, minBrightness, currentTime);
		}
	}
	
	protected virtual float ChangeSaturation(bool isSunrise) {
		return 1;
	}
	
	protected override void FadeIn() {
		renderer.material.SetFloat("Hue", ChangeHue(true));
		renderer.material.SetFloat("Brightness", ChangeBrightness(true));
		renderer.material.SetFloat("Saturation", ChangeSaturation(true));
	}
	
	protected override void FadeOut() {
		renderer.material.SetFloat("Hue", ChangeHue(false));
		renderer.material.SetFloat("Brightness", ChangeBrightness(false));
		renderer.material.SetFloat("Saturation", ChangeSaturation(false));
	}
	
	private void SetVertexColors(Color color, int startIndex, int endIndex) {
		while (startIndex < endIndex) {
			vertexColors[startIndex] = color;
			startIndex++;
		}
	}

}
