using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuakeIIEffect : EffectBlock 
{
	public float	duration 	= 0f;
	public float	force    	= 0f;
	public int		moveNumber	= 0;

	private float	_startTime	= 0f;

	protected override IEnumerator EffectSequence ()
	{
		float		t		= 0f;
		GameBlock[] blocks	= GameObject.FindObjectsOfType<GameBlock> ();
		float quakeForce 	= force;

		AttachBlocks (blocks);

		yield return null;

		Vector2 direction = Vector2.right;

		for (int i = 0; i < moveNumber; i++) 
		{
			Debug.Log ("apply dir " + direction);
			_startTime	= Time.time;

			while (t < 1f) 
			{
				t = (Time.time - _startTime) / duration;


				// SOMETHING DESTROY THE RB2D !??
				foreach (GameBlock block in blocks) {
					if (block.family == resistantFamily || 
						block.isAttached == true || 
						block.rigidBody2D == null ||
						block.collisionCount == 0)
						continue;

					block.rigidBody2D.AddForce (direction * quakeForce);
				}

				yield return null;
			}
			/*
			foreach (GameBlock block in blocks) 
			{
				if (block.family == resistantFamily || block.isAttached == true)
					continue;

				Vector2 velocity = block.rigidBody2D.velocity;
				velocity.x = 0f;
				block.rigidBody2D.velocity = velocity;
			}
			*/

			quakeForce		*= 1.5f;			
			direction.x *= -1;

			t = 0f;
		}
		/*
		foreach (GameBlock block in blocks) 
		{
			if (block.family == resistantFamily || block.isAttached == true || block.rigidBody2D == null )
				continue;

			block.rigidBody2D.velocity = Vector2.zero;
		}*/

		yield return new WaitForSeconds (0.5f);

		DettachBlocks (blocks);
	}	
}
