using UnityEngine;
using System.Collections;
using SmoothMoves;

public class TitleMenu : MonoBehaviour {
	public BoneAnimation tree;
	
	public UIPanel titleMenuPanel;
	public UISlider titleMenuSlider;
	
	public UIPanel mainMenuPanel;
	public UISlider newGameSlider;
	
	public UIPanel openningScenePanel;
	
	public Arrow[] arrows;
	
	// Use this for initialization
	void Start () {
		titleMenuSlider.sliderValue = 0;
		newGameSlider.sliderValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(titleMenuPanel.enabled) CheckTitleSlider();
		if(mainMenuPanel.enabled) CheckMainMenuSliders();
	}
	
	void CheckTitleSlider(){
		if(titleMenuSlider.sliderValue != 0 && !Input.GetMouseButton(0)){
			titleMenuSlider.sliderValue = 0;
		}
		if(titleMenuSlider.sliderValue == 1){
			tree.Play("WaveMiddle");
			TransitionPanels(titleMenuPanel, mainMenuPanel);
			foreach(Arrow arrow in arrows){
				arrow.DisableArrow();
			}
		}	
	}
	
	void CheckMainMenuSliders(){
		if(newGameSlider.sliderValue != 0 && !Input.GetMouseButton(0)){
			newGameSlider.sliderValue = 0;
		}
		if(newGameSlider.sliderValue == 1){
			tree.Play("WaveOld");
			TransitionPanels(mainMenuPanel, openningScenePanel);
		}
	}
	
	void TransitionPanels(UIPanel toDisable, UIPanel toEnable){
		Utils.SetActiveRecursively(toDisable.gameObject, false);
		Utils.SetActiveRecursively(toEnable.gameObject, true);	
	}
	
}
