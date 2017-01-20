using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBlock : MonoBehaviour 
{
	public BlockFamily _blockFamily = default(BlockFamily);

	private SpriteRenderer _spriteRenderer = null;

	void Awake ()
	{
		_spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Start()
	{
		_blockFamily = (BlockFamily)Random.Range (0f, (int)BlockFamily.Size);

		switch (_blockFamily) 
		{
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

		if (otherBlock != null && otherBlock._blockFamily == _blockFamily)
			JoinBlock (otherBlock, collison);
	}

	void JoinBlock(GameBlock block, Collision2D collision)
	{
		FixedJoint2D joint	= gameObject.AddComponent<FixedJoint2D> ();
		joint.connectedBody = block.gameObject.GetComponent<Rigidbody2D> ();
	}
}
