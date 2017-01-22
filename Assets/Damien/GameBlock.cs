using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBlock : MonoBehaviour 
{	
	public  PhysicsMaterial2D	physicMaterial2d	= null;
	public  bool				_isAttached			= false;

	private SpriteRenderer		_spriteRenderer		= null;
	private Rigidbody2D			_rigidBody2D		= null;
	private	BlockFamily			_family 			= default(BlockFamily);
	private List<GameBlock>		_nearBlocks			= new List<GameBlock>();
	private PolygonCollider2D	_polygonCollider	= null;

	public	Rigidbody2D		rigidBody2D 	{ get { return _rigidBody2D; } }
	public  SpriteRenderer	spriteRenderer	{ get { return _spriteRenderer; } }
	public  BlockFamily		family			{ get { return _family; } }
	public  bool            isAttached		{ get { return _isAttached; } }
    public Player catchPlayer;

    public Texture2D atlas;

    void Awake ()
	{
		_spriteRenderer 	= gameObject.GetComponent<SpriteRenderer> ();
		_rigidBody2D		= gameObject.GetComponent<Rigidbody2D> ();
		_polygonCollider	=	gameObject.GetComponent<PolygonCollider2D> ();
	}

	void Start()
	{
		//
	}

    private void Update() {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        if(screenPosition.y < 0) {
            Player[] players = Game.instance.players;
            foreach(Player player in players) {
                player.removeObject(this.gameObject);
            }
            GameObject.Find("Spawn Area").GetComponent<SpawnBlock>().deleteBlock(this.gameObject);
        }

        Color color = new Color(0f, 0f, 0f, 0f);
        switch(_family)
        {
            case BlockFamily.Cosmos:
                color = new Color(1f, 0f, 0f, 0f);
                break;

            case BlockFamily.Tsunami:
                color = new Color(0f, 1f, 0f, 0f);
                break;

            case BlockFamily.Wind:
                color = new Color(0f, 0f, 1f, 0f);
                break;

            case BlockFamily.Quake:
                color = new Color(0f, 0f, 0f, 1f);
                break;

        }
        _spriteRenderer.material.SetColor("_BlockType", color);
        _spriteRenderer.material.SetTexture("_BlockAtlas", atlas);
    }

    public void SetRandomFamily()
	{
		_family = (BlockFamily)Random.Range (1, (int)BlockFamily.Size);
        Debug.Log(_family);
		SetColor ();
	}

	public void SetFamily (BlockFamily family)
	{
		_family = family;
		SetColor ();
	}

	private void SetColor ()
	{
		//switch (family) 
		//{
		//	case BlockFamily.Wind:
		//		_spriteRenderer.color = Color.cyan;
		//	break;

		//	case BlockFamily.Tsunami:
		//		_spriteRenderer.color = Color.blue;
		//	break;

		//	case BlockFamily.Quake:
		//		_spriteRenderer.color = Color.yellow;
		//	break;

		//	case BlockFamily.Cosmos:
		//		_spriteRenderer.color = Color.green;
		//	break;

		//	default:
		//		_spriteRenderer.color = Color.white;
		//	break;
		//}
	}

	public void BecamePhysics()
	{
		_rigidBody2D.bodyType 		= RigidbodyType2D.Dynamic;
		_polygonCollider 			= GetComponent<PolygonCollider2D> ();
		_polygonCollider.isTrigger	= false;

	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (transform.position.y < -3.75f && GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
	{
        CheckNearObject(collision);
        checkCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        checkCollision(collision);
    }

    private void checkCollision(Collision2D collision) {
        // Si on est sous le seuil des bases, le bloc redevient neutre
        if(this.gameObject.transform.position.y < -2.25f) { // -2.5 de seuil + une sércurité
            Player[] players = Game.instance.players;
            for(int i = 0; i < players.Length; i++) {
                players[i].removeObject(this.gameObject);
            }
            this.catchPlayer = null;
        }

        if(this.catchPlayer == null) {
            return;
        }

        if(collision.gameObject.CompareTag("GameBlock") == true) {
            if(this.catchPlayer.hasObject(collision.gameObject)) {
                this.catchPlayer.addObject(this.gameObject);
            } else {
                this.catchPlayer.removeObject(this.gameObject);
            }

            this.catchPlayer = null;

        } else {
            this.catchPlayer.addObject(this.gameObject);
            this.catchPlayer = null;
        }
    }

    void CheckNearObject (Collision2D collision) {
        if(collision.gameObject.CompareTag("GameBlock") == true) {
            GameBlock block = collision.gameObject.GetComponent<GameBlock>();

            if(_nearBlocks.Contains(block) == false)
                _nearBlocks.Add(block);
        }
        }

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("GameBlock") == true) 
		{
			GameBlock block = collision.gameObject.GetComponent<GameBlock> ();

			_nearBlocks.Remove (block);
		}
	}

    private void addPlayers(Player[] players) {
        if(this.transform.position.x < 0 && players[1].hasObject(this.gameObject) == false) {
            players[0].addObject(this.gameObject);
        } else if(players[0].hasObject(this.gameObject) == false) {
            players[1].addObject(this.gameObject);
        }
    }

    private void removePlayers(Player[] players) {
        players[0].removeObject(this.gameObject);
        players[1].removeObject(this.gameObject);
    }

    public void JoinBlocks()
	{
		foreach (GameBlock near in _nearBlocks) 
		{
			FixedJoint2D joint	= gameObject.AddComponent<FixedJoint2D> ();
			joint.connectedBody = near.gameObject.GetComponent<Rigidbody2D> ();
			near.Attach (this);
		}
	}

	public void UnJoinBlock ()
	{
		FixedJoint2D[] allJoint = transform.GetComponentsInChildren<FixedJoint2D> ();

		foreach (FixedJoint2D joint in allJoint) 
		{
			joint.connectedBody.gameObject.GetComponent<GameBlock> ().Dettach ();
			GameObject.Destroy (joint);
		}
	}

	public void Attach(GameBlock root)
	{
		_isAttached = true;
		//_spriteRenderer.color = Color.magenta;
	}

	public void Dettach()
	{
		_isAttached = false;
		SetColor ();
	}
}
