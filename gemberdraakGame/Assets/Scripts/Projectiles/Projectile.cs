using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed;
	public MovementController mc;
	Rigidbody rb;
	public bool bounced = true;
	public bool inactive;

	void Awake(){
		rb = gameObject.GetComponent<Rigidbody> ();
	}

	void Update(){
		rb.velocity = transform.forward * speed ;

	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Player" && !inactive) {
			if (other.gameObject.GetComponent<MovementController>() != mc) {
				other.gameObject.GetComponent<MovementController> ().SetState(charState.STUNNED);
				other.gameObject.GetComponent<MovementController> ().anim.SetBool ("Movement", false);
				other.gameObject.GetComponent<MovementController> ().stuntime = 0;
				GetComponent<ParticleSystem> ().Stop ();
				StartCoroutine (DestroyProjectile ());
			}
		}
		if (other.gameObject.tag == "Wall") {
			if (bounced) {
				GetComponent<ParticleSystem> ().Stop ();
				StartCoroutine (DestroyProjectile ());
			} else {
				
			}
		}
	}

	IEnumerator DestroyProjectile(){
		speed = 0;
		inactive = true;
		yield return new WaitForSeconds (0.4f);
		Destroy (gameObject);
	}
}
