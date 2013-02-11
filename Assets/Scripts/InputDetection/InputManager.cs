using UnityEngine;
using System.Collections;

/*
 *  InputManager.cs
 * 		Will detect what type of input we should use and will continuously run it.
 * 
 */

public class InputManager : MonoBehaviour {
	#region Fields
	private InputType typeOfInput;
	#endregion
	
	void Start () {
		typeOfInput = DetermineTypeOfInput();
	}
	
	private InputType DetermineTypeOfInput() {
		// Check what type of input we should be expecting
		// if we are running on android or iOS then use touch controls else
		// it is assumed this game will be ran on mobile or computers and thus other device inputs are not accounted for
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer){
			return (new TouchInput());
		} else {
			return (new MouseInput());
		}
	}
	
	void Update () {
		typeOfInput.HandleInput();       
	}
}
