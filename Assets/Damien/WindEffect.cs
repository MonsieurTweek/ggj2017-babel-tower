using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : EffectBlock 
{
	public Vector2 direction = Vector2.zero;
	public float   force	 = 0f;

	protected override IEnumerator EffectSequence ()
	{	
		GameBlock[] blocks = GameObject.FindObjectsOfType<GameBlock> ();

		AttachBlocks (blocks);

		yield return null;

		foreach (GameBlock block in blocks) 
		{
			if (block.family == resistantFamily || block.isAttached == true)
				continue;

			Vector2 velocity			= block.rigidBody2D.velocity;
			velocity.x 					= 0f;
			block.rigidBody2D.velocity	= velocity;

			block.rigidBody2D.AddForce(direction * force, ForceMode2D.Impulse);
		}

		yield return new WaitForSeconds(0.5f);

		DettachBlocks (blocks);
	}
}
