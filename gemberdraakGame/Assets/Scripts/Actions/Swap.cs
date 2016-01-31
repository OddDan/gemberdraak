using UnityEngine;
using System.Collections;

public class Swap : MonoBehaviour {

	public Aura a;
	
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			a.AuraBurst(2);

			if (other.gameObject.GetComponent<MovementController> ().state == charState.FLYING) {
				other.gameObject.GetComponent<MovementController> ().SetState (charState.FLEEING);
				other.gameObject.GetComponent<MovementController> ().connectedPlayer.GetComponent<MovementController> ().Smite ();
				other.gameObject.GetComponent<MovementController> ().connectedPlayer = null;
				other.gameObject.GetComponent<MovementController> ().carrying = false;
			}
		}
	}

	void OnTriggerEnter(Collider other){

	}
}
