using System;
using UnityEngine;
using System.Collections.Generic;


public class DangerEngine
{
	private List<BlockFamily> blockFamilyList = new List<BlockFamily>();
	private Timer _dangerTimer	= null;

	public enum STATE { WAITING, WARNING, IN_PROGRESS };

	private STATE _currentState = STATE.WAITING;

	public DangerEngine ()
	{
		for (int i = 0; i < 20; i++) {
			blockFamilyList.Add((BlockFamily)UnityEngine.Random.Range (1, (int)BlockFamily.Size));
		}

		_dangerTimer = new Timer (0f);
		SwitchState (STATE.WAITING);
	}

	public BlockFamily currentDanger
	{
		get { return blockFamilyList[0]; }
	}

	public BlockFamily nextCurrentDanger
	{
		get { return blockFamilyList[1]; }
	}

	public STATE currentState
	{
		get { return _currentState; }
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

			GetNewDanger ();

			Debug.Log ("Danger stop");

			blockFamilyList.RemoveAt (0);

		}
		if (_currentState == STATE.WARNING) 
		{

            int _duration = 7;
			Game.instance.dangerWarning.SetDanger (currentDanger, _duration);	

			_dangerTimer.SetDuration (_duration);
			_dangerTimer.Start ();

			AudioManager.instance.alert.Play ();
			AudioManager.instance.mainMusic.Stop ();
			AudioManager.instance.mainMusic.PlayDelayed (2.8f);
			AudioManager.instance.mainMusic.pitch =  1.4f;
		}
		if (_currentState == STATE.IN_PROGRESS) 
		{
			LaunchDanger ();

			AudioManager.instance.mainMusic.Stop ();
			AudioManager.instance.mainMusic.PlayDelayed (3f);
			AudioManager.instance.mainMusic.pitch =  1f;

			Game.instance.dangerWarning.Hide ();

			_dangerTimer.SetDuration (3);
			_dangerTimer.Start ();
		}
		
	}

	private void GetNewDanger()
	{
		Debug.Log ("Launch danger in 10 sec " + currentDanger.ToString ());
	}

	private void LaunchDanger()
	{
		Debug.Log ("Launch danger " + currentDanger.ToString ());

		switch (currentDanger) 
		{
		case BlockFamily.Wind:
			Game.instance.windEffect.LaunchEffect ();
			AudioManager.instance.wind.Play ();
			break;
		case BlockFamily.Tsunami:
			Game.instance.tsunamiEffect.LaunchEffect ();
			AudioManager.instance.water.Play ();
			break;
		case BlockFamily.Quake:
			Game.instance.quakeEffect.LaunchEffect ();
			AudioManager.instance.quake.Play ();
			break;
		case BlockFamily.Cosmos:
			Game.instance.alienEffect.LaunchEffect ();
			AudioManager.instance.space.Play ();
			break;
		}
	}
}


