using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour 
{
	public GameObject[] blocPrefab = null;

	public float rumbleForce = 0f;
	public int rumbleCount = 0;
	public float rumbleFrequency = 0f;
    private Timer _popTimer = new Timer(0f);

    public bool spawnActivated = false;
    public float popDelay = 5.0f;

	public static int BLOCK_INDEX = 0;

    List<BlockSave> save = new List<BlockSave>();

	// Use this for initialization
	void Start () 
	{
        _popTimer.SetDuration(popDelay);
        _popTimer.Start();
    }

    void FixedUpdate()
    {
        if (spawnActivated == true && _popTimer.isFinished() == true)
        {
            Transform bloc = GameObject.Instantiate(blocPrefab[Random.Range(0, blocPrefab.Length)], transform).transform;
            bloc.localPosition = new Vector3(Random.Range(-8.5f, 8.5f), 0f, 0f);
            _popTimer.SetDuration(popDelay);
            _popTimer.Start();
        }
    }
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKeyDown (KeyCode.S)) 
		{
			Transform bloc = GameObject.Instantiate (blocPrefab[Random.Range(0,blocPrefab.Length)], transform).transform;

			bloc.localPosition		= new Vector3 (Random.Range(-0.5f, 0.5f) - bloc.localScale.x, 0f, 0f);
            bloc.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
            bloc.localScale = new Vector3(Random.Range(1f, 3f), Random.Range(0.6f, 1.2f), 1f);

			bloc.gameObject.name = "block_" + (++BLOCK_INDEX).ToString ("0000");

            bloc.GetComponent<GameBlock> ().SetRandomFamily ();
		}	

		if (Input.GetKeyDown (KeyCode.I))
			DestroyBlock (BlockFamily.Cosmos);
		else if (Input.GetKeyDown (KeyCode.O))
			DestroyBlock (BlockFamily.Quake);
		else if (Input.GetKeyDown (KeyCode.P))
			DestroyBlock (BlockFamily.Tsunami);		
		else if (Input.GetKeyDown(KeyCode.U))
			DestroyBlock(BlockFamily.Wind);

		if (Input.GetKeyDown (KeyCode.C))
			CopyBlocks ();

		if (Input.GetKeyDown (KeyCode.V))
			StartCoroutine (PasteBlocks ());
	}

	void DestroyBlock(BlockFamily family)
	{
		Debug.Log ("Destroy " + family);

		GameBlock[] blocks = GameObject.FindObjectsOfType<GameBlock> ();

		foreach (GameBlock block in blocks) 
			if (block.family == family)
				GameObject.Destroy (block.gameObject);		
	}

	void CopyBlocks ()
	{
		save.Clear ();

		GameBlock[] blocks = GameObject.FindObjectsOfType<GameBlock> ();

		foreach (GameBlock block in blocks) 
			save.Add (new BlockSave (block));
	}

	IEnumerator PasteBlocks ()
	{
		GameBlock[] blocks = GameObject.FindObjectsOfType<GameBlock> ();
		foreach (GameBlock block in blocks) 
		{
			GameObject.Destroy (block.gameObject);
		}

		List<GameBlock> pasteGameBlock = new List<GameBlock> ();

		foreach (BlockSave blockSave in save) 
		{
			GameObject gameBlockObject	= GameObject.Instantiate (blocPrefab[0]);
			GameBlock gameBlock			= gameBlockObject.GetComponent<GameBlock> ();

			gameBlockObject.GetComponent<SpriteRenderer> ().sprite = blockSave.sprite;

			gameBlock.SetFamily( blockSave.family);

			gameBlockObject.transform.position = blockSave.position;
			gameBlockObject.transform.rotation = blockSave.rotation;
			gameBlockObject.transform.localScale = blockSave.scale;

			GameObject.Destroy (gameBlockObject.GetComponent<MoveBlock> ());
			GameObject.Destroy (gameBlockObject.GetComponent<PolygonCollider2D> ());

			pasteGameBlock.Add (gameBlock);

			yield return null;
		}

		foreach (GameBlock pastBlock in pasteGameBlock) 
		{
			pastBlock.gameObject.AddComponent<PolygonCollider2D> ();

			pastBlock.BecamePhysics ();

			pastBlock.gameObject.GetComponent<PolygonCollider2D> ().sharedMaterial = pastBlock.physicMaterial2d;

			pastBlock.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;	
		}
	}
}



public class BlockSave
{
	public Sprite		sprite;
	public Vector3		position;
	public Quaternion	rotation;
	public Vector3 		scale;
	public BlockFamily	family;

	public BlockSave(GameBlock block)
	{
		sprite		= block.GetComponent<SpriteRenderer> ().sprite;
		position	= block.transform.position;
		rotation 	= block.transform.rotation;
		scale 		= block.transform.localScale;
		family 		= block.family;
	}
}