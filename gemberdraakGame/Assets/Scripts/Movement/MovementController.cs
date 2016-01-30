using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	public float movementSpeed = 10f;
	public float gravity = 9.87f;
	public float jumpSpeed;
	public float acceleration = 8;
	public string ctrlName = "CTRL1_";
	public float stuntime = 2f;
	public float throwSpeed = 30;

	public charType type = charType.PRIEST;

	public GameObject body;
	public GameObject priestModel;
	public GameObject sheepModel;

	public float maxStruggle;
	public float struggle;

	public GameObject connectedPlayer;
	public bool carrying = false;
	public bool flying = false;

	public Vector3 velocity;
	float verticalSpeed;
	float zSpeed;
	float xSpeed;
	public bool canMove = true;

	void Start () {
		velocity = Vector3.zero;
	}

	void Update () {
		
		CheckType ();
		DoMovement ();

	}

	public void SetStun(){
		StartCoroutine (Stun ());
	}

	IEnumerator Stun(){
		canMove = false;
		if(carrying){
			carrying = false;
			connectedPlayer.gameObject.GetComponent<MovementController> ().carrying = false;
			connectedPlayer.gameObject.GetComponent<MovementController> ().connectedPlayer = null;
			connectedPlayer = null;
		}
		yield return new WaitForSeconds (stuntime);
		canMove = true;
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
		target.y = 0;
		if (target.normalized != Vector3.zero) {
			if (type == charType.PRIEST || type == charType.SHEEP) {
				body.transform.rotation = Quaternion.LookRotation (target.normalized, Vector3.up);
			}if (type == charType.SHEEP && carrying) {
				body.transform.rotation = Quaternion.LookRotation (target.normalized, Vector3.down);
				body.transform.rotation *= Quaternion.Euler (0, -90, 0);
			}
		}
	}

	void CheckType(){
		if (type == charType.PRIEST) {
			priestModel.SetActive (true);
			sheepModel.SetActive (false);
			body = priestModel;
		}
		if (type == charType.SHEEP) {
			CalculateStruggle ();
			priestModel.SetActive (false);
			sheepModel.SetActive (true);
			body = sheepModel;
		}
	}

	void DoMovement(){
		if (canMove) {
			if (carrying && type == charType.SHEEP) {

			} else {
				zSpeed = AccelerateTowards (zSpeed, acceleration, Input.GetAxis (ctrlName + "vertical") * movementSpeed);
				xSpeed = AccelerateTowards (xSpeed, acceleration, Input.GetAxis (ctrlName + "horizontal") * movementSpeed);

				velocity = new Vector3 (xSpeed, 0, zSpeed);
				Vector3 dir = new Vector3 (xSpeed, 0, zSpeed);


				CharacterController controller = gameObject.GetComponent<CharacterController> ();
				if (controller.isGrounded) {
					verticalSpeed = 0;
					if (Input.GetButtonDown (ctrlName + "jump") && type == charType.SHEEP) {
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
		}

		if (carrying && type == charType.SHEEP){
			SetLookRotation (connectedPlayer.GetComponent<MovementController>().body.transform.forward);
			Vector3 targetPos = connectedPlayer.transform.position;
			targetPos.y += 3.5f;
			transform.position = targetPos;
		}
	}

	void CalculateStruggle(){
		if (carrying) {
			if (struggle < 0) {
				struggle = 0;
			} else {
				struggle -= 20 * Time.deltaTime;
			}
		}
	}

	public IEnumerator Flying(float forward, float height){
		canMove = false;
		carrying = false;
		Vector3 dir = connectedPlayer.GetComponent<MovementController> ().velocity;

		CharacterController controller = gameObject.GetComponent<CharacterController> ();
		controller.Move (Vector3.up);
		verticalSpeed = (height);
		while (!controller.isGrounded) {
			velocity = dir.normalized * forward;
			verticalSpeed -= gravity * Time.deltaTime;
			velocity.y = verticalSpeed;
			controller.Move (velocity * Time.deltaTime);
			yield return null;
		}
		canMove = true;
	}
}

public enum charType{
	PRIEST,
	SHEEP
}

