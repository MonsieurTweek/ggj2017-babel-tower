using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game instance;

	public Player player;

	public int 	score = 0;


	public enum STATE
	{
		INTRO, GAME
	}

	public STATE state { get { return _state;}}

	private STATE _state;
	private Timer _stateTimer = new Timer(0f);

	public void Awake(){
		instance = this;
#if UNITY_EDITOR
		Application.targetFrameRate = 60;
#endif
	}


	// Use this for initialization
	public void Start () {
		SwitchState(STATE.INTRO);
	}
	
	// Update is called once per frame
	public void Update () {
		
		if(_stateTimer.isFinished() == true)
		{
			if(_state == STATE.INTRO)
			{
				SwitchState(STATE.GAME);
			}

			if(_state == STATE.GAME)
			{
				// Do things 


			}
		}


	}

	public void Reset()
	{
	}


	private void SwitchState(STATE state)
	{
		_state = state;

		if(state == STATE.INTRO)
		{
			_stateTimer.SetDuration(2f);
			_stateTimer.Start();
		}
		else if(state == STATE.GAME)
		{
			AudioManager.instance.mainMusic.Play();
		}

		//_stateTimer.SetDuration(0.7f);
		//_stateTimer.Start();
	}



	public void Pause()
	{
	}

	public void Resume()
	{
		SwitchState(STATE.GAME);
	}
}
