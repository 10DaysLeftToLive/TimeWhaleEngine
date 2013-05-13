using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassiveChatToPlayer : ManagerSingleton<PassiveChatToPlayer> {
	private static Queue<string> whatToSayQueue;
	private string whatToSay;
	
	public override void Init() {
		whatToSayQueue = new Queue<string>();
	}
	
	public string GetTextToSay() {
		if (whatToSayQueue.Count <= 0) {
			return (whatToSayQueue.Dequeue());
		}
		
		return ChooseGenericText();
	}
	
	public void AddTextToSay(string toSay) {
		whatToSayQueue.Enqueue(toSay);
	}
	
	private string ChooseGenericText() {
		switch(Random.Range(1, 3)) {
			case 1:
				return "How's it going?";
				break;
			
			case 2:
				return "Hello";
				break;
			
			case 3:
				return "Salutations";
				break;
			
			default:
				return "Hi";
				break;
		}
	}
}
