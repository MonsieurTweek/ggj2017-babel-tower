using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Gamepad : MonoBehaviour {

    // Exposed value to Tweek !
    public float turnSpeed = 5.0f;
    public float moveSpeed = 50.0f;

    // Drag&Drop
    private bool dragging = false;
    private float distance;
    private GameObject target;
    private Vector3 screenPosition;
    private Vector3 offset;
    public int rayLength = 10;

    // Gamepad
    private PlayerIndex playerIndex;
    private bool playerIndexSet = false;
    public GamePadState state;
    public GamePadState prevState;

    // Sprite renderer
    private SpriteRenderer sprite;

    private GameObject GetTargetObject(out RaycastHit hit)
    {
        GameObject target = null;
        if (Physics.Raycast(transform.position, new Vector3(0.0f, 0.0f, 1.0f * rayLength), out hit))
        {
            target = hit.collider.gameObject;
        }
        Debug.Log(target);
        return target;
    }

    // Use this for initialization
    void Start () {

        // Cache-cache
        sprite = GetComponent<SpriteRenderer>();
        sprite.material.SetColor("_Color", Color.red);
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

    }

    void FixedUpdate ()
    {

        // Make the current object turn
        // Priority order :
        // 1/ Trigger left turn left
        // 2/ Trigger right turn right
        if(state.Triggers.Left > 0 && this.target != null)
        {
        } else if(state.Triggers.Right > 0 && this.target != null)
        {
        }

        // Make the current object move
        transform.position += new Vector3(state.ThumbSticks.Left.X * moveSpeed * Time.deltaTime, state.ThumbSticks.Left.Y * moveSpeed * Time.deltaTime);

        // Grab an element
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            RaycastHit hitInfo;
            this.target = GetTargetObject(out hitInfo);
            if (this.target != null)
            {
                sprite.material.SetColor("_Color", Color.green);
                this.dragging = true;

                //Convert world position to screen position.
                this.screenPosition = Camera.main.WorldToScreenPoint(this.target.transform.position);
                this.offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(transform.position.x, transform.position.y, this.screenPosition.z));
            }
        } else if(this.dragging == true && prevState.Buttons.A == ButtonState.Released)
        {
            sprite.material.SetColor("_Color", Color.red);
            this.dragging = false;
            this.target = null;
        }

        // Drag the element
        if (this.dragging == true)
        {
            //track mouse position.
            Vector3 currentScreenSpace = new Vector3(transform.position.x, transform.position.y, this.screenPosition.z);

            //convert screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + this.offset;

            //It will update target gameobject's current postion.
            this.target.transform.position = new Vector3(transform.position.x, transform.position.y, this.screenPosition.z);
        }

    }
}
