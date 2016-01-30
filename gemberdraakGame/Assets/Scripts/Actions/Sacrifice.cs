using UnityEngine;
using System.Collections;

public class Sacrifice : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<MovementController> ().type == charType.SHEEP){
			other.gameObject.GetComponent<MovementController> ().score += 1;
			other.gameObject.GetComponent<MovementController> ().Respawn ();
		}
	}

	void OnTriggerExit(Collider other){

	}
}
