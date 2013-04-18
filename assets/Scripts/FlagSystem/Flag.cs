using UnityEngine;
using System.Collections;

public class Flag {
	private string _name;
	private bool _isSetOff;
	
	public Flag(string name){
		_name = name;
		_isSetOff = false;
	}
	
	public bool Equals(string name){
		return (_name == name);	
	}
	
	public void SetOff(){
		Debug.Log("Setting off " + _name);
		_isSetOff = true;	
	}
	
	public void UnSet(){
		Debug.Log("Unsetting " + _name);
		_isSetOff = false;
	}
}
