using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game instance;
	private Timer _stateTimer = new Timer(0f);
	private DangerEngine _dangerEngine = null;

	public void Awake(){
		instance = this;
#if UNITY_EDITOR
		Application.targetFrameRate = 60;
#endif
	}


	// Use this for initialization
	public void Start () {
        _stateTimer.SetDuration(10.0f);
        _stateTimer.Start();

		_dangerEngine  = new DangerEngine();
    }
	
	// Update is called once per frame
	public void Update () {

		_dangerEngine.Update ();

		if(_stateTimer.isFinished() == true)
		{
            Debug.Log("GameOver");
        }

    }
}
