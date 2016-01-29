using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	public float movementSpeed = 10f;
	public float gravity = 9.87f;
	public float jumpSpeed;
	public float acceleration = 8;

	Vector3 velocity;

	public string ctrlName = "CTRL1_";


	private float verticalSpeed;

	float upSpeed;
	float horizontalSpeed;

	// Use this for initialization
	void Start () {
		velocity = Vector3.zero;
	}

	// Update is called once per frame
	void Update () {
		upSpeed = AccelerateTowards (upSpeed, acceleration, Input.GetAxis (ctrlName + "vertical") * movementSpeed);
		horizontalSpeed = AccelerateTowards (horizontalSpeed, acceleration, Input.GetAxis (ctrlName + "horizontal") * movementSpeed);

		Vector3 direction = new Vector3(horizontalSpeed,0,upSpeed);

		velocity = direction;


		CharacterController controller = gameObject.GetComponent<CharacterController> ();
		if (controller.isGrounded) {
			verticalSpeed = 0;
			if(Input.GetButtonDown(ctrlName+"jump")){
				verticalSpeed = jumpSpeed;
			}
		} 

		verticalSpeed -= gravity * Time.deltaTime;
		velocity.y = verticalSpeed;
		controller.Move (velocity * Time.deltaTime);
	}

	float AccelerateTowards(float speed, float acceleration, float targetSpeed){
		if (speed == targetSpeed) {
			return speed;
		} else {
			float dir = Mathf.Sign (targetSpeed - speed);	
			speed += acceleration * Time.deltaTime * dir;
			//if (Mathf.Sign (targetSpeed) != Mathf.Sign(speed)) {
			//	return 0;
			//} else {
				return (dir == Mathf.Sign (targetSpeed - speed)) ? speed : targetSpeed;
			//}
		}
	}
}

