using UnityEngine;
using System.Collections;

[System.Serializable]
public class TimeSwitchObject{
	public string objectLabel;
	
	public GameObject youngTimeObject;
	public GameObject middleTimeObject;
	public GameObject oldTimeObject;
	
	public bool staticInYoung = false;
	public bool staticInMiddle = false;
	public bool staticInOld = false;
	
	public void ChangeAge(CharacterAgeState newAge){
		if (!IsStaticIn(newAge)){
			middleTimeObject.transform.localPosition = youngTimeObject.transform.localPosition;
		}
	}
	
	public GameObject GetTimeObjectAt(CharacterAgeState state){
		switch (state){
			case (CharacterAgeState.YOUNG):
				return (youngTimeObject);
			case (CharacterAgeState.MIDDLE):
				return (middleTimeObject);
			case (CharacterAgeState.OLD):
				return (oldTimeObject);
			default:
				return (null); 
		}
	}
	
	private bool IsStaticIn(CharacterAgeState state){
		switch (state){
			case (CharacterAgeState.YOUNG):
				return (staticInYoung);
			case (CharacterAgeState.MIDDLE):
				return (staticInMiddle);
			case (CharacterAgeState.OLD):
				return (staticInOld);
			default:	
				return (true); 
		}
	}
}