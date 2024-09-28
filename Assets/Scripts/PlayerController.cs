using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private Rigidbody rb;
	private float movementX;
	private float movementY;
	private bool isJumping = false;
	private bool isGrounded = true;
	private bool delayCheck = false;
	
	public float speed = 3;
	
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>(); //Assign the player rigidbody as rb
	}
	
	void FixedUpdate()
	{
		//Add player movement to rigidbody
		Vector3 rollingVector = new Vector3(movementX, 0.0f, movementY);
		rollingVector *= speed; //Add a force multiplier for some UMMPH
		
		//Determine jumping force value
		//Debug.Log("PlayerController.cs : player y value = " + transform.position.y);
		if (isJumping && isGrounded)
		{
			rollingVector.y = 200;
			isGrounded = false;
			StartCoroutine(JumpDelay()); //Incorporate delay before checking the landing
		}
		
		rb.AddForce(rollingVector);
	}
	
	//Movement Functions
	void OnMove(InputValue movementValue)
	{
		Vector2 movementVector = movementValue.Get<Vector2>();
		movementX = movementVector.x;
		movementY = movementVector.y;
		
		//Debug.Log("PlayerController.cs : movementValue = " + movementVector);
	}
	
	void OnJump(InputValue inputValue)
	{
		if (!isJumping && isGrounded)
		{
			isJumping = true; //Set as active
			//Debug.Log("PlayerController.cs : Jump key pressed");
		}
	}
	
	void OnCollisionStay()
	{
		if (isJumping && !isGrounded && delayCheck) //Check if the player has landed, after delay timing
		{
			isGrounded = true;
			isJumping = false;
			delayCheck = false;
			//Debug.Log("PlayerController.cs : Player landed");	
		}
	}
	
	IEnumerator JumpDelay() //Used to add a delay before checking the landing
	{
		yield return new WaitForSeconds(0.5f);
		//Debug.Log("Jump delay finished");
		delayCheck = true;
	}
	
	//Collision Functions
 void OnTriggerEnter(Collider otherObj)
 {
 	if (otherObj.gameObject.CompareTag("PickupObj"))
	{
		Debug.Log("PlayerController.cs : Player collided with Obj " + otherObj.transform.name);
		Destroy(otherObj.gameObject);
	}
 }
}
