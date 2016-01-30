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
		if (Input.GetButtonDown ("CTRL" + mc.playerID + "_jump") && mc.type == charType.PRIEST) {
			if (!mc.carrying && mc.canMove) {
				if (lastFireTime < Time.realtimeSinceStartup - cooldownTime) {
					lastFireTime = Time.realtimeSinceStartup;
					Vector3 rotation = mc.lastLookDir;
					rotation.y = 0;
					GameObject projectileShot = GameObject.Instantiate (projectile, transform.position, Quaternion.LookRotation (rotation.normalized, Vector3.up)) as GameObject;
					Projectile pro = projectileShot.GetComponent<Projectile> ();
					pro.mc = mc;
				}
			}
		}
		if (Input.GetButtonDown ("CTRL" + mc.playerID + "_action")) {
			if (mc.type == charType.PRIEST) {
				if (!mc.carrying) {
					RaycastHit[] hits;
					hits = Physics.SphereCastAll (transform.position, grabSize, mc.body.transform.forward, grabDistance);
					foreach (RaycastHit hit in hits) {
						if (hit.collider.gameObject.tag == "Player") {
							if (hit.collider.gameObject.GetComponent<MovementController> ().type == charType.SHEEP) {
								mc.connectedPlayer = hit.collider.gameObject;
								mc.connectedPlayer.GetComponent<MovementController> ().connectedPlayer = gameObject;
								mc.connectedPlayer.GetComponent<MovementController> ().carrying = true;
								mc.connectedPlayer.GetComponent<MovementController> ().SetState (charState.CARRIED);
								mc.carrying = true;
							}
						}
					}
				} else {
					mc.connectedPlayer.GetComponent<MovementController> ().SetState(charState.FLYING);
					mc.connectedPlayer.GetComponent<MovementController> ().lastLookDir = mc.lastLookDir;
					mc.connectedPlayer.GetComponent<MovementController> ().verticalSpeed = 15;
					mc.connectedPlayer = null;
					mc.carrying = false;
				}
			}
			if(mc.state == charState.CARRIED){
				mc.struggle += 10;
				if (mc.struggle >= mc.maxStruggle) {
					SetFree ();
				}
			}
		}
	}

	void SetFree(){
		mc.lastLookDir = mc.connectedPlayer.GetComponent<MovementController> ().lastLookDir;
		// set to players direction not the direction of the carrying priest.
		mc.SetState(charState.FLYING);
		mc.connectedPlayer.GetComponent<MovementController> ().SetState (charState.STUNNED);
		mc.connectedPlayer.GetComponent<MovementController> ().stuntime = 0;
		mc.connectedPlayer.GetComponent<MovementController> ().connectedPlayer = null;
		mc.connectedPlayer.GetComponent<MovementController> ().carrying = false;
		mc.struggle = 0;
	}

}
