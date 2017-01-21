using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBlock : MonoBehaviour 
{
	public BlockFamily _blockFamily = default(BlockFamily);

	private SpriteRenderer _spriteRenderer = null;

	public PhysicsMaterial2D physicMaterial2d;

	public List<GameBlock> nearBlock = new List<GameBlock>();

	public bool attach = false;

	void Awake ()
	{
		_spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Start()
	{
	}

	public void SetRandomFamily()
	{
		_blockFamily = (BlockFamily)Random.Range (0f, (int)BlockFamily.Size);
		SetColor ();
	}

	public void SetFamily (BlockFamily family)
	{
		_blockFamily = family;
		SetColor ();
	}

	private void SetColor ()
	{
		switch (_blockFamily) {
		case BlockFamily.Fire:
			_spriteRenderer.color = Color.red;
			break;

		case BlockFamily.Water:
			_spriteRenderer.color = Color.blue;
			break;

		case BlockFamily.Shock:
			_spriteRenderer.color = Color.yellow;
			break;
		}	
	}

	void OnCollisionEnter2D(Collision2D collison)
	{	
		
		GameBlock otherBlock = collison.gameObject.GetComponent<GameBlock> ();

		if (otherBlock != null)
			nearBlock.Add (otherBlock);
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		GameBlock otherBlock = collision.gameObject.GetComponent<GameBlock> ();

		if (otherBlock != null)
			nearBlock.Remove (otherBlock);
	}

	public void JoinBlocks()
	{
		foreach (GameBlock near in nearBlock) 
		{
			FixedJoint2D joint	= gameObject.AddComponent<FixedJoint2D> ();
			joint.connectedBody = near.gameObject.GetComponent<Rigidbody2D> ();
			near.attach = true;
		}
	}

	public void UnJoinBlock ()
	{
		foreach (GameBlock near in nearBlock) 
		{
			near.attach = false;
		}

		FixedJoint2D[] allJoint = transform.GetComponentsInChildren<FixedJoint2D> ();
		foreach (FixedJoint2D joint in allJoint)
			GameObject.Destroy (joint);
	}
}
