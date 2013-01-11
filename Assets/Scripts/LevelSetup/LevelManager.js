#pragma strict

function Awake () {
	ScreenSetup.instance.CalculateSettings();
	this.gameObject.AddComponent("FramesPerSecond");
}

function Start () {
	
}

function OnGUI(){
	
}

function Update () {

}