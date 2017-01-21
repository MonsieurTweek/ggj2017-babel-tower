using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectBlock : MonoBehaviour 
{
	public KeyCode		keyCode 		= default(KeyCode);
	public BlockFamily	resistantFamily	= default(BlockFamily);
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (keyCode))
			LaunchEffect ();
	}
		
	public virtual void LaunchEffect ()
	{
		StartCoroutine (EffectSequence ());
	}

	public void AttachBlocks (GameBlock[] blocks)
	{
		foreach (GameBlock block in blocks) 
		{
			if (block.family == resistantFamily) 
			{
				block.GetComponent<Rigidbody2D> ().mass = 10f;	
				block.JoinBlocks();
			}
		}
	}

	public void DettachBlocks (GameBlock[] blocks)
	{
		foreach (GameBlock block in blocks) 
		{
			if (block.family == resistantFamily) 
			{
				block.GetComponent<Rigidbody2D> ().mass = 1f;
				block.UnJoinBlock();
			}
		}		
	}

	protected abstract IEnumerator EffectSequence ();
}
