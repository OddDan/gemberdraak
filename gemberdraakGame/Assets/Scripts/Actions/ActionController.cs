using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {

	public GameObject projectile;
	public float cooldownTime = 4f;
	float lastFireTime = 0;
	MovementController mc;

	public float grabDistance=1;
	public float grabSize=1;

	void Awake(){
		mc = gameObject.GetComponent<MovementController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown (mc.ctrlName + "jump") && mc.type == charType.PRIEST) {
			if (!mc.carrying && mc.canMove) {
				if (lastFireTime < Time.realtimeSinceStartup - cooldownTime) {
					lastFireTime = Time.realtimeSinceStartup;
					Vector3 rotation = mc.velocity;
					rotation.y = 0;
					GameObject projectileShot = GameObject.Instantiate (projectile, transform.position, Quaternion.LookRotation (rotation.normalized, Vector3.up)) as GameObject;
					Projectile pro = projectileShot.GetComponent<Projectile> ();
					pro.mc = mc;
				}
			}
		}
		if (Input.GetButtonDown (mc.ctrlName + "action") && mc.type == charType.PRIEST) {
			if (!mc.carrying) {
				RaycastHit[] hits;
				hits = Physics.SphereCastAll (transform.position, grabSize, mc.body.transform.forward, grabDistance);
				foreach (RaycastHit hit in hits) {
					if (hit.collider.gameObject.tag == "Player") {
						if (hit.collider.gameObject.GetComponent<MovementController> ().type == charType.SHEEP) {
							mc.connectedPlayer = hit.collider.gameObject;
							mc.connectedPlayer.GetComponent<MovementController> ().connectedPlayer = gameObject;
							mc.connectedPlayer.GetComponent<MovementController> ().carrying = true;
							mc.carrying = true;
						}
					}
				}
			} else {
				StartCoroutine(mc.connectedPlayer.GetComponent<MovementController>().Flying(mc.throwSpeed, 22));
				mc.connectedPlayer = null;
				mc.carrying = false;
			}
		}

		if (Input.GetButtonDown (mc.ctrlName + "action") && mc.type == charType.SHEEP && mc.carrying) {
			mc.struggle += 10;
			if (mc.struggle >= mc.maxStruggle) {
				SetFree ();
			}
		}

	}

	void SetFree(){
		StartCoroutine (mc.Flying(10,5));
		mc.connectedPlayer.GetComponent<MovementController> ().SetStun ();
		mc.struggle = 0;
	}

}
