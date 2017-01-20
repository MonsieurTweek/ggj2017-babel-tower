using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamepadController : MonoBehaviour {

    public float turnSpeed = 100.0f;
    public float moveSpeed = 50.0f;

    private bool playerIndexSet = false;
    private PlayerIndex playerIndex;
    private GamePadState state;
    private GamePadState prevState;

    // Use this for initialization
    void Start () {
		
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
        if(state.Triggers.Left > 0)
        {
            transform.localRotation *= Quaternion.Euler(0.0f, 0.0f, state.Triggers.Left * turnSpeed * Time.deltaTime);
        } else if(state.Triggers.Right > 0)
        {
            transform.localRotation *= Quaternion.Euler(0.0f, 0.0f, -1 * state.Triggers.Right * turnSpeed * Time.deltaTime);
        }

        // Make the current object move
        transform.position += new Vector3(state.ThumbSticks.Left.X * moveSpeed * Time.deltaTime, state.ThumbSticks.Left.Y * moveSpeed * Time.deltaTime);

    }
}
