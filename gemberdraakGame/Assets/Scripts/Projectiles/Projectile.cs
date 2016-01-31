using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed;
	public MovementController mc;
	Rigidbody rb;
	public bool bounced = true;
	public bool inactive;
	public AudioSource hit;
	public AudioSource fire;
	public GameObject explosion;

	void Awake(){
		rb = gameObject.GetComponent<Rigidbody> ();
		fire.pitch = Random.Range (0.8f, 1.2f);
	}

	void Update(){
		rb.velocity = transform.forward * speed ;

	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Player" && !inactive) {
			if (other.gameObject.GetComponent<MovementController>() != mc) {
				if (other.gameObject.GetComponent<MovementController> ().type == charType.SHEEP && !other.gameObject.GetComponent<MovementController> ().isDemonLord) {
					other.gameObject.GetComponent<MovementController> ().SetState (charState.STUNNED);
				} else {
					other.gameObject.GetComponent<MovementController> ().SetState (charState.KNOCKBACK);
					other.gameObject.GetComponent<MovementController> ().verticalSpeed = 5;
					other.gameObject.GetComponent<MovementController> ().lastLookDir = transform.forward*-1;
				}
				other.gameObject.GetComponent<MovementController> ().anim.SetBool("Movement", false);
				other.gameObject.GetComponent<MovementController> ().stuntime = 0;
				GetComponent<ParticleSystem> ().Stop();
				StartCoroutine (DestroyProjectile ());
				fire.Stop ();
				hit.Play ();
			}
		}
		if (other.gameObject.tag == "Wall") {
			if (bounced) {
				GetComponent<ParticleSystem> ().Stop ();
				StartCoroutine (DestroyProjectile ());
				fire.Stop ();
				hit.Play ();
			} else {
				
			}
		}
	}

	IEnumerator DestroyProjectile(){
		speed = 0;
		inactive = true;
		Instantiate (explosion, transform.position, Quaternion.identity);
		yield return new WaitForSeconds (0.6f);
		Destroy (gameObject);
	}
}
