using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed;
	public MovementController mc;
	Rigidbody rb;


	void Awake(){
		rb = gameObject.GetComponent<Rigidbody> ();
	}

	void Update(){
		rb.velocity = transform.forward * speed ;
	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Player") {
			if (other.gameObject.GetComponent<MovementController>() != mc) {
				other.gameObject.GetComponent<MovementController> ().SetState(charState.STUNNED);
				other.gameObject.GetComponent<MovementController> ().stuntime = 0;
				Destroy (gameObject);
			}
		}
		if (other.gameObject.tag == "world") {
			Destroy (gameObject);
		}
	}
}
