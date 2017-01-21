using System;
using UnityEngine;


public class DangerEngine
{
	private BlockFamily _blockFamily = default(BlockFamily);
	private Timer _dangerTimer	= null;

	private enum STATE { WAITING, WARNING, IN_PROGRESS };

	private STATE _currentState = STATE.WAITING;

	public DangerEngine ()
	{
		_dangerTimer = new Timer (0f);
		SwitchState (STATE.WAITING);
	}


	public void Update()
	{
		if (_currentState == STATE.WAITING) 
		{
			if (_dangerTimer.isFinished () == true) 
			{
				SwitchState (STATE.WARNING);
			}
		}
		if (_currentState == STATE.WARNING) 
		{
			if (_dangerTimer.isFinished () == true) 
			{
				SwitchState (STATE.IN_PROGRESS);
			}
		}

		if (_currentState == STATE.IN_PROGRESS) 
		{
			if (_dangerTimer.isFinished () == true) 
			{
				SwitchState (STATE.WAITING);
			}
		}
			
	}



	private void SwitchState (STATE state)
	{
		_currentState = state;

		if (_currentState == STATE.WAITING) 
		{
			_dangerTimer.SetDuration (UnityEngine.Random.Range (10, 15));
			_dangerTimer.Start ();

			//Debug.Log ("Danger stop");
		}
		if (_currentState == STATE.WARNING) 
		{
			GetNewDanger ();

			_dangerTimer.SetDuration (10);
			_dangerTimer.Start ();
		}
		if (_currentState == STATE.IN_PROGRESS) 
		{
			LaunchDanger ();

			_dangerTimer.SetDuration (3);
			_dangerTimer.Start ();
		}
		
	}

	private void GetNewDanger()
	{

		_blockFamily = (BlockFamily)UnityEngine.Random.Range (1, (int)BlockFamily.Size);

		//Debug.Log ("Launch danger in 10 sec " + _blockFamily.ToString ());
	}

	private void LaunchDanger()
	{
		//Debug.Log ("Launch danger " + _blockFamily.ToString ());

		switch (_blockFamily) 
		{
		case BlockFamily.Wind:
			break;
		case BlockFamily.Tsunami:
			break;
		case BlockFamily.Quake:
			break;
		case BlockFamily.Cosmos:
			break;
		}
	}
}


