using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuakeEffect : EffectBlock 
{
	public float force = 0f;

	protected override IEnumerator EffectSequence ()
	{
		GameBlock[] blocks = GameObject.FindObjectsOfType<GameBlock> ();

		AttachBlocks (blocks);

		yield return null;

		Vector2 direction = new Vector2 (Random.Range (-0.8f, 0.8f), 1f);

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
