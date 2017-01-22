using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour 
{
	public GameObject[] blocPrefab = null;
	public int[] blocRandomWeight = null;

	public float rumbleForce = 0f;
	public int rumbleCount = 0;
	public float rumbleFrequency = 0f;
    private Timer _popTimer = new Timer(0f);

    public bool spawnActivated = false;
    public float popDelay = 5.0f;
    private bool fastPop = false;
    public int seuil = 3;

	public float ratioSpecial = 0f;

	public static int BLOCK_INDEX = 0;
	public static int SPAWN_AT_START = 5;
	public static float MIN_X_SPAWN = -7f;
	public static float MAX_X_SPAWN = 7f;

    List<BlockSave> save = new List<BlockSave>();

    public int maxBlocs = 20;
    public List<GameObject> blockList = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
        _popTimer.SetDuration(popDelay);
        _popTimer.Start();

		for (int i = 0; i < blocRandomWeight.Length - 1; i++) 
		{
			blocRandomWeight[i + 1] += blocRandomWeight [i];
		}

		for (int i = 0; i < SPAWN_AT_START; i++) 
		{
			Spawn (i);
		}
    }

    void FixedUpdate()
    {
        if(blockList.Count <= seuil) {
            if(fastPop == false) {
                _popTimer.Stop();
                _popTimer.SetDuration(getFastPopDelay());
                _popTimer.Start();
                fastPop = true;
            } else {
                _popTimer.SetDuration(getFastPopDelay());
            }
        }
        if (spawnActivated == true && _popTimer.isFinished() == true)
        {
			Spawn ();
        }
    }

	private void Spawn(int position = -1)
	{
        freeBlock();

        int random = Random.Range(0, blocRandomWeight[blocRandomWeight.Length - 1]);
        int selectedBloc = 0;

        for(int i = 0; i < blocRandomWeight.Length; i++) {
            if(selectedBloc < blocRandomWeight[i]) {
                selectedBloc = i;
                break;
            }
        }

        Transform bloc = GameObject.Instantiate(blocPrefab[Random.Range(0, blocPrefab.Length)], transform).transform;

		bloc.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));

		if (bloc.gameObject.name.StartsWith ("Square") == false) 
		{
			bloc.localScale = new Vector3(Random.Range(2, 4) / 2f, Random.Range(2, 4) / 2f, 1f);
		}
		else
		{
			bloc.localScale = new Vector3(1f, 1f, 1f);
		}


        if(position != -1) {
            bloc.localPosition = new Vector3(MAX_X_SPAWN * 2f * (((float)position) / (SPAWN_AT_START - 1)) + MIN_X_SPAWN, 0f, 0f);
        } else {
            bloc.localPosition = new Vector3(Random.Range(MIN_X_SPAWN, MAX_X_SPAWN), 0f, 0f);
        }

        bloc.gameObject.name = "block_" + (++BLOCK_INDEX).ToString("0000");
        blockList.Add(bloc.gameObject);

        if(blockList.Count > seuil && fastPop == true) {
            fastPop = false;
        }

        if(fastPop == false) {
            _popTimer.SetDuration(popDelay);
        } else {
            _popTimer.SetDuration(getFastPopDelay());
        }
		_popTimer.Start();

		bloc.gameObject.name = "block_" + (++BLOCK_INDEX).ToString("0000");

		if (Random.Range (0f, 1f) < ratioSpecial)
		{
			bloc.GetComponent<GameBlock> ().SetRandomFamily ();

			//bloc.localScale = new Vector3(1f, 1f, 1f);
		}
	}

    private float getFastPopDelay() {
        return (blockList.Count * 100 / maxBlocs) * popDelay / 100;
    }

    public void freeBlock() {
        if(blockList.Count >= maxBlocs) {
            foreach(GameObject objectToDelete in blockList) {
                if(objectToDelete.GetComponent<GameBlock>().catchPlayer == null) {
                    deleteBlock(objectToDelete);
                    break;
                }
            }
        }
    }

    public void deleteBlock(GameObject block) {
        blockList.Remove(block);
        Destroy(block);
    }

    public void addBlock(GameObject block) {
        freeBlock();
        blockList.Add(block);
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

			gameBlockObject.name = "block_" + (++BLOCK_INDEX).ToString ("0000");

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