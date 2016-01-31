using UnityEngine;
using System.Collections;

public class Sacrifice : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<MovementController> ().type == charType.SHEEP){
			GameManager._GM.ReleaseSoul(other.gameObject.GetComponent<MovementController> ().playerID);
			GameManager._GM.playerScores[other.gameObject.GetComponent<MovementController>().playerID-1] += 1;
			other.gameObject.GetComponent<MovementController> ().Respawn ();
		}
	}

	void OnTriggerExit(Collider other){

	}
}
