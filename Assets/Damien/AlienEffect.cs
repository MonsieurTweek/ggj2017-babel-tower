using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEffect : EffectBlock 
{
	public float	duration 	= 0f;
	public float	force    	= 0f;

	private float	_startTime	= 0f;

	protected override IEnumerator EffectSequence ()
	{
		float		t		= 0f;
		GameBlock[] blocks	= GameObject.FindObjectsOfType<GameBlock> ();

		_startTime			= Time.time;

		AttachBlocks (blocks);

		yield return null;

		while (t < 1f) 
		{
			t = (Time.time - _startTime) / duration;

			foreach (GameBlock block in blocks) 
			{
				if (block.family == resistantFamily || block.isAttached == true)
					continue;

				block.rigidBody2D.AddForce (Vector2.up * force);
			}

			yield return null;	
		}

		foreach (GameBlock block in blocks) 
		{
			if (block.family == resistantFamily || block.isAttached == true)
				continue;

			block.rigidBody2D.velocity = Vector2.zero;
		}

		yield return new WaitForSeconds (0.5f);

		DettachBlocks (blocks);
	}	
}
