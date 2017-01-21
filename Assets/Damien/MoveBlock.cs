using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockFamily
{
	Water,
	Fire,
	Shock,
	Size
}

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
			gameObject.GetComponent<PolygonCollider2D> ().isTrigger = false;
			gameObject.AddComponent<Rigidbody2D> ();


			GameObject.Destroy (this);
		}
	}
}
