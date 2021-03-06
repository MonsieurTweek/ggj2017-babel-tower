﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerControl : MonoBehaviour {

    // Exposed value to Tweek !
    public float turnSpeed = 5.0f;
    public float moveSpeed = 15.0f;

    // Drag&Drop
    private bool dragging = false;
    private float distance;
    private GameObject target;
    private Vector3 screenPosition;
    private Vector3 offset;
    public int rayLength = 10;

    // Gamepad
    public PlayerIndex playerIndex;
    private bool playerIndexSet = false;
    public GamePadState state;
    public GamePadState prevState;

    public bool recordInput = false;

    private GameObject GetTargetObject(out RaycastHit2D hit)
    {
        GameObject target = null;
        hit = Physics2D.Raycast(transform.position, new Vector3(0.0f, 0.0f, 1.0f * rayLength));
        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
        }
        Debug.Log(target);
        return target;
    }

    // Use this for initialization
    void Start () {
        recordInput = false;
        playerIndex = GetComponent<Player>().playerIndex;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(recordInput == true) {
            prevState = state;
            state = GamePad.GetState(playerIndex);
        }

    }

    void FixedUpdate ()
    {
        if(recordInput == false) {
            return;
        }

        // Make the current object turn
        // Priority order :
        // 1/ Trigger left turn left
        // 2/ Trigger right turn right
        if(state.Triggers.Left > 0 && this.target != null)
        {
            this.target.transform.localRotation *= Quaternion.Euler(0.0f, 0.0f, turnSpeed);
        } else if(state.Triggers.Right > 0 && this.target != null)
        {
            this.target.transform.localRotation *= Quaternion.Euler(0.0f, 0.0f, -turnSpeed);
        }

        // Make the current object move
        if(Mathf.Abs(state.ThumbSticks.Left.X) > 0 || Mathf.Abs(state.ThumbSticks.Left.Y) > 0)
        {
            Vector3 movement = new Vector3(state.ThumbSticks.Left.X * moveSpeed * Time.deltaTime, state.ThumbSticks.Left.Y * moveSpeed * Time.deltaTime, 0.0f);
            Vector3 newPositionInWorld = transform.position + movement;
            Vector3 newPositionInScreen = Camera.main.WorldToScreenPoint(transform.position + movement);
            if (newPositionInScreen.x < 0 || newPositionInScreen.x > Screen.width)
            {
                movement.x = 0;
            }
            if (newPositionInScreen.y < 0 || newPositionInScreen.y > Screen.height)
            {
                movement.y = 0;
            }
            transform.position += movement;
        }

        // Grab an element
        if(prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed) {
            RaycastHit2D hitInfo;
            this.target = GetTargetObject(out hitInfo);


            if(
                this.target != null &&
                target.CompareTag("GameBlock") == true &&
				isOwnedByEnemy(target) == false &&
                target.GetComponent<GameBlock>().catchPlayer == null
               ) {
                this.dragging = true;

                //Convert world position to screen position.
                this.screenPosition = Camera.main.WorldToScreenPoint(this.target.transform.position);
                this.offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(transform.position.x, transform.position.y, 0.0f));

                this.disablePhysics(target);
            }
        } else if(
            this.dragging == true &&
            prevState.Buttons.A == ButtonState.Released &&
			isOwnedByEnemy(target) == false &&
            target.GetComponent<GameBlock>().catchPlayer.Equals(GetComponent<Player>()) == true
        )
        {
            this.dragging = false;

            if( ((int)playerIndex == 0 && target.transform.position.x < 0) || ((int)playerIndex == 1 && target.transform.position.x > 0) )
            {
                this.enablePhysics(target);
            } else
            {
                target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            this.target = null;
        }

        // Drag the element
        if (this.dragging == true)
        {
            //It will update target gameobject's current postion.
            this.target.transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        }

    }

    private bool isOwned(GameObject target) {
        Player[] players = Game.instance.players;
        bool isOwned = false;

        foreach(Player player in players) {
            isOwned = isOwned || player.hasObject(target);
        }

        return isOwned;
    }

	private bool isOwnedByEnemy(GameObject target) {
		Player[] players = Game.instance.players;
		bool isOwned = false;

		foreach(Player player in players) {
			if (player.playerIndex != playerIndex)
				isOwned = isOwned || player.hasObject(target);
		}

		return isOwned;
	}

    private void disablePhysics(GameObject target) {
        target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        target.GetComponent<Collider2D>().isTrigger = true;
        target.GetComponent<GameBlock>().catchPlayer = this.GetComponent<Player>();
    }

    private void enablePhysics(GameObject target) {
        target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        target.GetComponent<Collider2D>().isTrigger = false;
    }
}
