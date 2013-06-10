using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dialogue {
	public int _npc;
	private string _textToSay;
	private string _animation;
	
	public string _TextToSay {
		get {return _textToSay;}
	}
	
	public string _AnimationToPlay {
		get { return _animation; }
	}
	
	public Dialogue(int npc, string textToSay) {
		_npc = npc;
		_textToSay = textToSay;
	}
	
	public Dialogue(int npc, string textToSay, string animation) {
		_npc = npc;
		_textToSay = textToSay;
		_animation = animation;
	}
}
