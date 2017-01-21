using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBlock : MonoBehaviour 
{	
	public  PhysicsMaterial2D	physicMaterial2d	= null;
	public  bool				_isAttached			= false;

	private SpriteRenderer		_spriteRenderer		= null;
	private Rigidbody2D			_rigidBody2D		= null;
	private	BlockFamily			_family 			= default(BlockFamily);
	private List<GameBlock>		_nearBlocks			= new List<GameBlock>();
	private PolygonCollider2D	_polygonCollider	= null;

	public	Rigidbody2D		rigidBody2D 	{ get { return _rigidBody2D; } }
	public  SpriteRenderer	spriteRenderer	{ get { return _spriteRenderer; } }
	public  BlockFamily		family			{ get { return _family; } }
	public  bool            isAttached		{ get { return _isAttached; } }

	void Awake ()
	{
		_spriteRenderer 	= gameObject.GetComponent<SpriteRenderer> ();
		_rigidBody2D		= gameObject.GetComponent<Rigidbody2D> ();
		_polygonCollider	=	gameObject.GetComponent<PolygonCollider2D> ();
	}

	void Start()
	{
		//
	}

	public void SetRandomFamily()
	{
		_family = (BlockFamily)Random.Range (0f, (int)BlockFamily.Size);
		SetColor ();
	}

	public void SetFamily (BlockFamily family)
	{
		_family = family;
		SetColor ();
	}

	private void SetColor ()
	{
		switch (family) 
		{
			case BlockFamily.Wind:
				_spriteRenderer.color = Color.cyan;
			break;

			case BlockFamily.Tsunami:
				_spriteRenderer.color = Color.blue;
			break;

			case BlockFamily.Quake:
				_spriteRenderer.color = Color.yellow;
			break;

			case BlockFamily.Cosmos:
				_spriteRenderer.color = Color.green;
			break;

			default:
				_spriteRenderer.color = Color.white;
			break;
		}
	}

	public void BecamePhysics()
	{
		_rigidBody2D.bodyType 		= RigidbodyType2D.Dynamic;
		_polygonCollider 			= GetComponent<PolygonCollider2D> ();
		_polygonCollider.isTrigger	= false;

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("GameBlock") == true) 
		{
			GameBlock block = collision.gameObject.GetComponent<GameBlock> ();

			if (_nearBlocks.Contains(block) == false)
				_nearBlocks.Add (block);
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("GameBlock") == true) 
		{
			GameBlock block = collision.gameObject.GetComponent<GameBlock> ();

			_nearBlocks.Remove (block);
		}
	}

	public void JoinBlocks()
	{
		foreach (GameBlock near in _nearBlocks) 
		{
			FixedJoint2D joint	= gameObject.AddComponent<FixedJoint2D> ();
			joint.connectedBody = near.gameObject.GetComponent<Rigidbody2D> ();
			near.Attach (this);
		}
	}

	public void UnJoinBlock ()
	{
		FixedJoint2D[] allJoint = transform.GetComponentsInChildren<FixedJoint2D> ();

		foreach (FixedJoint2D joint in allJoint) 
		{
			joint.connectedBody.gameObject.GetComponent<GameBlock> ().Dettach ();
			GameObject.Destroy (joint);
		}
	}

	public void Attach(GameBlock root)
	{
		_isAttached = true;
		_spriteRenderer.color = Color.magenta;
	}

	public void Dettach()
	{
		_isAttached = false;
		SetColor ();
	}
}
