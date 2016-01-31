using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
	public float priestSpeed = 6.5f;
	public float priestAcceleration = 25;
	public float sheepSpeed = 9f;
	public float sheepAcceleration = 35;

	public float lightningRadius = 6f;

	public Animator sheepAnim;
	public Animator priestAnim;
	public Animator anim;

	float movementSpeed = 10f;
	float gravity = 30f;
	public float jumpSpeed;

	float acceleration = 8;
	public int playerID = 1;
	public float stuntime = 1.5f;
	public float maxStunDuration = 1.5f;
	public float throwSpeed = 10;

	public charType type = charType.PRIEST;
	public charState state = charState.MOVEMENT;

	public GameObject body;
	public GameObject priestModel;
	public GameObject sheepModel;

	public float maxStruggle;
	public float struggle;

	public Vector3 lastLookDir;

	public GameObject connectedPlayer;
	public bool carrying = false;
	public bool flying = false;

	public Vector3 velocity;
	public float verticalSpeed;
	float zSpeed;
	float xSpeed;

	bool inBounds = true;

	public bool canMove = true;

	void Start () {
		velocity = Vector3.zero;
		anim.SetBool ("Movement", true);
	}

	void Update () {
		CalculateStruggle ();
		DoMovement ();
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

	void LightningStrike(){
		RaycastHit[] hits;
		hits = Physics.SphereCastAll (transform.position, lightningRadius, Vector3.up, 1);
		foreach (RaycastHit hit in hits) {
			Debug.Log (GameObject.Find ("Lightning").GetComponent<Lightning> ());
			GameObject.Find ("Lightning").GetComponent<Lightning> ().CastLightning (transform.position);
			if (hit.transform.tag == "Player" && hit.transform.gameObject.GetComponent<MovementController> ().playerID != playerID && hit.transform.gameObject.GetComponent<MovementController> ().type != charType.SHEEP) {
				hit.transform.gameObject.GetComponent<MovementController> ().SetState (charState.KNOCKBACK);
				hit.transform.gameObject.GetComponent<MovementController> ().lastLookDir = transform.position - hit.transform.position;
				hit.transform.gameObject.GetComponent<MovementController> ().verticalSpeed = 15;
				hit.transform.gameObject.GetComponent<MovementController> ().anim.SetBool ("Movement", false);

			}
		}
	}

	public void Mutate(int type){
		if (type == 0) {
			this.type = charType.PRIEST;
			movementSpeed = priestSpeed;
			acceleration = priestAcceleration;
			priestModel.SetActive (true);
			sheepModel.SetActive (false);
			body = priestModel;
			anim = priestAnim;
		}
		if (type == 1) {
			this.type = charType.SHEEP;
			movementSpeed = sheepSpeed;
			acceleration = sheepAcceleration;
			priestModel.SetActive (false);
			sheepModel.SetActive (true);
			body = sheepModel;
			anim = sheepAnim;
			LightningStrike ();
		}
	}
		
	void DoMovement(){
		CharacterController controller = gameObject.GetComponent<CharacterController> ();
		//switch == with states.

		switch (state) {
		case charState.MOVEMENT:
			zSpeed = AccelerateTowards (zSpeed, acceleration, Input.GetAxis ("CTRL" + playerID + "_vertical") * movementSpeed);
			xSpeed = AccelerateTowards (xSpeed, acceleration, Input.GetAxis ("CTRL" + playerID + "_horizontal") * movementSpeed);

			velocity = new Vector3 (xSpeed, 0, zSpeed);
			anim.SetFloat ("Speed", velocity.magnitude);
			if (velocity.magnitude > 1f) {
				lastLookDir = new Vector3 (xSpeed, 0, zSpeed).normalized;
			}

			if (controller.isGrounded) {
				verticalSpeed = 0;
				if (Input.GetButtonDown ("CTRL" + playerID + "_jump") && type == charType.SHEEP) {
					anim.SetTrigger ("Jump");
					GetComponent<ActionController> ().pickup.Play ();
					verticalSpeed = jumpSpeed;
				}
			} 
			verticalSpeed -= gravity * Time.deltaTime;
			velocity.y = verticalSpeed;

			SetLookRotation (lastLookDir);

			controller.Move (velocity * Time.deltaTime);
			break;
		case charState.CARRIED:
			
			SetLookRotation (connectedPlayer.GetComponent<MovementController> ().body.transform.forward);
			Vector3 targetPos = connectedPlayer.transform.position;
			targetPos.y += 2.5f;
			targetPos += connectedPlayer.GetComponent<MovementController> ().body.transform.forward * 0.7f;
			transform.position = targetPos;

			break;
		case charState.FLYING:
			verticalSpeed -= gravity * Time.deltaTime;

			velocity = lastLookDir.normalized * 10;
			velocity.y = verticalSpeed;

			controller.Move (velocity * Time.deltaTime);

			if (controller.isGrounded) {
				carrying = false;
				if (state != charState.FLEEING) {
					SetState (charState.MOVEMENT);
					anim.SetBool ("Carrying", false);
					anim.SetBool ("Movement", true);
				}
			}

			break;
		case charState.FLEEING:
			if (!controller.isGrounded) {
				velocity = lastLookDir.normalized * 10;
			}
			if (controller.isGrounded) {
				carrying = false;
				velocity = transform.position * 15 * Time.deltaTime;
				SetLookRotation (velocity);
			}
			verticalSpeed -= gravity * Time.deltaTime;
			velocity.y = verticalSpeed;
			anim.SetFloat ("Speed", velocity.magnitude);
			controller.Move (velocity * Time.deltaTime);
			if (Vector3.Distance (transform.position, Vector3.zero) > 35) {
				Mutate (0);
				Respawn ();
			}

			break;
		case charState.ENTERING:
			velocity = (transform.position * -1) * (15 * Time.deltaTime);
			lastLookDir = velocity.normalized;
			verticalSpeed -= gravity * Time.deltaTime;
			velocity.y = verticalSpeed;
			anim.SetFloat ("Speed", velocity.magnitude);
			controller.Move (velocity * Time.deltaTime);
			SetLookRotation (lastLookDir);
			if (Vector3.Distance (transform.position, Vector3.zero) < 25) {
				xSpeed = 0;
				zSpeed = 0;
				SetState (charState.MOVEMENT);
				anim.SetBool ("Movement", true);
			}
			break;
		case charState.STUNNED:
			stuntime += Time.deltaTime;
			if (stuntime > maxStunDuration) {
				SetState (charState.MOVEMENT);
				anim.SetBool ("Movement", true);
			} 
			if (carrying) {
				anim.SetBool ("Carrying", false);
				carrying = false;
				connectedPlayer.GetComponent<MovementController> ().SetState (charState.FLYING);
			}
			break;
		case charState.KNOCKBACK:
			stuntime += Time.deltaTime;

			verticalSpeed -= gravity * Time.deltaTime;

			velocity = lastLookDir.normalized * 10 * -1;
			velocity.y = verticalSpeed;

			controller.Move (velocity * Time.deltaTime);

			if (controller.isGrounded) {
				anim.SetBool ("Movement", true);
				SetState (charState.MOVEMENT);
			}
			break;
		default:
			break;
		}

		// Out of bounds
		// Vector2 v2 = new Vector2(transform.position.x, transform.position.z); 
		if(type == charType.SHEEP && state == charState.FLEEING){
			gameObject.layer = LayerMask.NameToLayer("NotBlockedByInvis");
		}else{
			gameObject.layer = LayerMask.NameToLayer("BlockedByInvis");
		}
	}

	public void SetState(charState newState){
		state = newState;
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

	public Vector3 GetSpawnPoint(){
		Vector3 v = GameManager._GM.soulStaches[playerID-1].transform.position * 0.8f; //Quaternion.Euler(0, Random.Range(0, 359), 0) * new Vector3(0, 0, 25);
		return v;
	}

	public void Respawn(){
		SetState (charState.ENTERING);
		anim.SetBool ("Movement", true);
		transform.position = GetSpawnPoint();
		Camera.main.GetComponent<CameraZoom>().SetFocus(playerID-1, GameManager._GM.players[playerID-1]);
		
	}		

	public void RemoveConnection(){
		carrying = false;
		connectedPlayer.gameObject.GetComponent<MovementController> ().carrying = false;
		connectedPlayer.gameObject.GetComponent<MovementController> ().connectedPlayer = null;
		connectedPlayer = null;
	}

	public void Smite(){
		//Fancy push animation
		Mutate(1);
		gameObject.GetComponent<ActionController> ().transforming.Play ();
	}
}

public enum charType{
	PRIEST,
	SHEEP
}

public enum charState{
	IDLE,
	MOVEMENT,
	CARRIED,
	FLYING,
	FLEEING,
	ENTERING,
	STUNNED,
	KNOCKBACK
}