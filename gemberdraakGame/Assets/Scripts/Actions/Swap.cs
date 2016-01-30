using UnityEngine;
using System.Collections;

public class Swap : MonoBehaviour {

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			if (other.gameObject.GetComponent<MovementController> ().state == charState.FLYING) {
				other.gameObject.GetComponent<MovementController> ().SetState (charState.FLEEING);
				other.gameObject.GetComponent<MovementController> ().connectedPlayer.GetComponent<MovementController> ().Smite ();
			}
		}
	}

	void OnTriggerEnter(Collider other){

	}
}
