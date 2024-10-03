using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
	public GameObject player;
	
	// Start is called before the first frame update
	void Start()
	{
		transform.position = player.transform.position;
	}

	// LateUpdate is called once per frame, but after all the other updates are done
	void Update()
	{
		transform.position = player.transform.position; //Move the follower based off the player movement
	}
}
