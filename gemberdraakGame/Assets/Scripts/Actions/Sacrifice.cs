using UnityEngine;
using System.Collections;

public class Sacrifice : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<MovementController> ().type == charType.SHEEP && !other.gameObject.GetComponent<MovementController> ().isDemonLord){
			GameManager._GM.ReleaseSoul(other.gameObject.GetComponent<MovementController> ().playerID);
			GameManager._GM.playerScores[other.gameObject.GetComponent<MovementController>().playerID-1] += 1;

			// After death animation
			other.gameObject.GetComponent<MovementController> ().transform.position = new Vector3(2*other.gameObject.GetComponent<MovementController> ().playerID, -10, 0);
		}
	}

	void OnTriggerExit(Collider other){

	}
}
