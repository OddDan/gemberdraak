using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<MovementController> ().type == charType.PRIEST){
			other.gameObject.GetComponent<MovementController> ().Kill();			
		}
	}

	void OnTriggerExit(Collider other){

	}

	void Update(){
		transform.position = transform.parent.position;
	}
}
