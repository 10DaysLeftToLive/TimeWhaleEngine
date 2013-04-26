using UnityEngine;
using System.Collections;

public class AgeSwapDetector {
	private static float RAYCAST_Z_OFFSET = -2;
	
	public static bool CheckTransitionPositionSuccess(Vector3 playerCenter, CharacterController charControl){
		Vector3 linecastTopLeftStart = new Vector3(playerCenter.x - charControl.radius, playerCenter.y + (charControl.height * 0.5f),playerCenter.z - charControl.radius + RAYCAST_Z_OFFSET);
		Vector3 linecastTopLeftEnd = new Vector3(playerCenter.x - charControl.radius, playerCenter.y + (charControl.height * 0.5f), playerCenter.z + charControl.radius);
		
		Vector3 linecastTopRightStart = new Vector3(playerCenter.x + charControl.radius, playerCenter.y + (charControl.height * 0.5f),playerCenter.z - charControl.radius + RAYCAST_Z_OFFSET);
		Vector3 linecastTopRightEnd = new Vector3(playerCenter.x + charControl.radius, playerCenter.y + (charControl.height * 0.5f), playerCenter.z + charControl.radius);
		
		Vector3 linecastBottomLeftStart = new Vector3(playerCenter.x - charControl.radius, playerCenter.y - (charControl.height * 0.5f),playerCenter.z - charControl.radius + RAYCAST_Z_OFFSET);
		Vector3 linecastBottonLeftEnd = new Vector3(playerCenter.x - charControl.radius, playerCenter.y - (charControl.height * 0.5f), playerCenter.z + charControl.radius);
		
		Vector3 linecastBottomRightStart = new Vector3(playerCenter.x + charControl.radius, playerCenter.y - (charControl.height * 0.5f),playerCenter.z - charControl.radius + RAYCAST_Z_OFFSET);
		Vector3 linecastBottomRightEnd = new Vector3(playerCenter.x + charControl.radius, playerCenter.y - (charControl.height * 0.5f), playerCenter.z + charControl.radius);
		
		RaycastHit hit = new RaycastHit();
		
		if(Physics.Linecast(linecastTopLeftStart, linecastTopLeftEnd, out hit) ||
			Physics.Linecast(linecastTopRightStart, linecastTopRightEnd, out hit) ||
			Physics.Linecast(linecastBottomLeftStart, linecastBottonLeftEnd, out hit) ||
			Physics.Linecast(linecastBottomRightStart, linecastBottomRightEnd, out hit)){
			if(hit.transform.GetComponent<SkinnedMeshRenderer>().enabled == true){
				if(hit.transform.tag == Strings.tag_Pushable ||
					hit.transform.tag == Strings.tag_Block ){
					return false;	
				}
			}
		}
		
		return true;	
		
	}
}
