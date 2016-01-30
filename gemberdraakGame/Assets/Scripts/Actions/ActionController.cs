using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {

	public GameObject projectile;
	public float cooldownTime = 4f;
	float lastFireTime = 0;
	MovementController mc;

	void Awake(){
		mc = gameObject.GetComponent<MovementController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(mc.ctrlName + "jump") && mc.type == charType.PRIEST) {
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
}
