using UnityEngine;
using System.Collections;

public class MetricsRecorder : MonoBehaviour {
	// Use this for initialization
	void Start () {
		SetupClickListening();
	}
	
	public static void RecordInteraction(string NPC, string item, float dispositionChange){
		Debug.Log("Recorded interaction with " + NPC + " using " + item + ". DispositionChange: " + dispositionChange);
	}
	
	private void RecordClick(EventManager EM, ClickPositionArgs e){
		Debug.Log("Recorded click at " + e.position);
	}
	
	private void RecortClickOnObject(EventManager EM, ClickedObjectArgs e){
		Debug.Log("Recorded Click on " + e.clickedObject.name);
	}
	
	private void SetupClickListening(){
		EventManager.instance.mOnClickEvent += new EventManager.mOnClickDelegate (RecordClick);
		EventManager.instance.mOnClickObjectEvent += new EventManager.mOnClickedObjectDelegate (RecortClickOnObject);
	}
}
