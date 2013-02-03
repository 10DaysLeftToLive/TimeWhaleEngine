using UnityEngine;
using System.Collections;

[System.Serializable]
public class Capsule{
	public float height;
	public float radius;
	public Vector3 center;
	
	public Capsule(float _height, float _radius, Vector3 _center){
		height = _height;
		radius = _radius;
		center = _center;
	}
}
