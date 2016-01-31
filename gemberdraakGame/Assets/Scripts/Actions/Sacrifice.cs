using UnityEngine;
using System.Collections;

public class Sacrifice : MonoBehaviour {

	public GameObject gore;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<MovementController> ().type == charType.SHEEP && !other.gameObject.GetComponent<MovementController> ().isDemonLord){
			other.gameObject.GetComponent<MovementController> ().SetState (charState.IDLE);
			GameManager._GM.ReleaseSoul(other.gameObject.GetComponent<MovementController> ().playerID);
			GameManager._GM.playerScores[other.gameObject.GetComponent<MovementController>().playerID-1] += 1;
			Instantiate (gore, Vector3.up * 2, Quaternion.identity);

			// After death animation
			other.gameObject.GetComponent<MovementController> ().transform.position = new Vector3(2*other.gameObject.GetComponent<MovementController> ().playerID, -10, 0);
		}
	}

	void OnTriggerExit(Collider other){

	}
}
