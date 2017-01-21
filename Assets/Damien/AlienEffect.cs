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
		float time = Time.time;

		yield return null;
	}	
}
