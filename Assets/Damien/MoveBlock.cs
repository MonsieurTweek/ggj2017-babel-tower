using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MoveBlock : MonoBehaviour 
{
	void FixedUpdate () 
	{
		Vector3 pos = transform.position;

		pos.x += Time.deltaTime * 5f * Input.GetAxis("Horizontal");
		pos.y += Time.deltaTime * 5f * Input.GetAxis ("Vertical");

		transform.position = pos;

		if (Input.GetKey (KeyCode.R))
			transform.Rotate (Vector3.forward, 90f * Time.deltaTime);

		if (Input.GetKey (KeyCode.Space)) 
		{
			gameObject.GetComponent<GameBlock> ().BecamePhysics ();


			GameObject.Destroy (this);
		}
	}
}
