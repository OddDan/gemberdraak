using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	public float movementSpeed = 10f;
	public float gravity = 9.87f;
	public float jumpSpeed;
	public float acceleration = 8;
	public string ctrlName = "CTRL1_";

	public GameObject body;

	Vector3 velocity;
	float verticalSpeed;
	float zSpeed;
	float xSpeed;

	void Start () {
		velocity = Vector3.zero;
	}

	void Update () {
		zSpeed = AccelerateTowards (zSpeed, acceleration, Input.GetAxis (ctrlName + "vertical") * movementSpeed);
		xSpeed = AccelerateTowards (xSpeed, acceleration, Input.GetAxis (ctrlName + "horizontal") * movementSpeed);

		velocity = new Vector3(xSpeed,0,zSpeed);
		Vector3 dir = new Vector3 (xSpeed, 0, zSpeed);

		CharacterController controller = gameObject.GetComponent<CharacterController> ();
		if (controller.isGrounded) {
			verticalSpeed = 0;
			if(Input.GetButtonDown(ctrlName+"jump")){
				verticalSpeed = jumpSpeed;
			}
		} 
		verticalSpeed -= gravity * Time.deltaTime;
		velocity.y = verticalSpeed;

		if (velocity.magnitude > 1) {
			SetLookRotation (dir);
		}

		controller.Move (velocity * Time.deltaTime);
	}

	float AccelerateTowards(float speed, float acceleration, float targetSpeed){
		if (speed == targetSpeed) {
			return speed;
		} else {
			float dir = Mathf.Sign (targetSpeed - speed);	
			speed += acceleration * Time.deltaTime * dir;
			return (dir == Mathf.Sign (targetSpeed - speed)) ? speed : targetSpeed;
		}
	}

	void SetLookRotation(Vector3 target){
		Debug.Log(target.normalized);
		target.y = 0;
		if (target.normalized != Vector3.zero) {
			body.transform.rotation = Quaternion.LookRotation (target.normalized, Vector3.down);
		}
	}
}

