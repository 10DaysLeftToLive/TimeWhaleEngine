using UnityEngine;
using System.Collections;

[System.Serializable]
public class TimeSwitchObject{
	public GameObject youngTimeObject;
	public GameObject middleTimeObject;
	public GameObject oldTimeObject;
	
	public bool staticInYoung = false;
	public bool staticInMiddle = false;
	public bool staticInOld = false;
	
	public Vector3 YoungPosition{
		get{return youngTimeObject.transform.localPosition;}
	}
}
