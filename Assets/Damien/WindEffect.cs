using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : EffectBlock 
{
	public Vector2 direction = Vector2.zero;
	public float   force	 = 0f;
    public Animator feedbackAnimator;

	protected override IEnumerator EffectSequence ()
	{	
		GameBlock[] blocks = GameObject.FindObjectsOfType<GameBlock> ();

		AttachBlocks (blocks);

		yield return null;

        feedbackAnimator.enabled = true;
        feedbackAnimator.Play("FeedbackAnimation", -1, 0f);
        Game.instance.ToggleVibration(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));

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

        Game.instance.ToggleVibration();

        DettachBlocks (blocks);
	}
}
