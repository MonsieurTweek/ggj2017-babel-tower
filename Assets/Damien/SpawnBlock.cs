using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour 
{
	public GameObject[] blocPrefab = null;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.S)) 
		{
			Transform bloc = GameObject.Instantiate (blocPrefab[Random.Range(0,blocPrefab.Length)], transform).transform;

			bloc.localPosition		= Vector3.zero;
			bloc.localEulerAngles	= new Vector3 (0f, 0f, Random.Range (0f, 360f));
			bloc.localScale			= new Vector3 (Random.Range (1f, 3f), Random.Range (0.6f, 1.2f), 1f);
		}	

		if (Input.GetKeyDown (KeyCode.I))
			DestroyBlock (BlockFamily.Fire);
		else if (Input.GetKeyDown (KeyCode.O))
			DestroyBlock (BlockFamily.Shock);
		else if (Input.GetKeyDown (KeyCode.P))
			DestroyBlock (BlockFamily.Water);		
	}

	void DestroyBlock(BlockFamily family)
	{
		Debug.Log ("Destroy " + family);

		GameBlock[] blocks = GameObject.FindObjectsOfType<GameBlock> ();

		foreach (GameBlock block in blocks) 
			if (block._blockFamily == family)
				GameObject.Destroy (block.gameObject);		
	}
}
