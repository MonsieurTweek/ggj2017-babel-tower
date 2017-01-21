using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game instance;

	private Timer _Timer = new Timer(0f);
    private int elapsedTime = 0; // in seconds
    public int countdownTime = 120;// in seconds

    // UI Elements
    public GameObject countdown;
    private Text countdownLabel;

	private DangerEngine _dangerEngine = null;

	public void Awake(){
		instance = this;
#if UNITY_EDITOR
		Application.targetFrameRate = 60;
#endif
	}


	// Use this for initialization
	public void Start () {
        countdownLabel = countdown.GetComponent<Text>();
        countdownLabel.text = formateElapsedTime(countdownTime);
        _Timer.SetDuration(1.0f);
        _Timer.Start();

		_dangerEngine  = new DangerEngine();
    }
	
	// Update is called once per frame
	public void Update () {

        _dangerEngine.Update();
		
		if(_Timer.isFinished() == true)
        {
            elapsedTime++;
            _Timer.SetDuration(1.0f);
            _Timer.Start();
            countdownLabel.text = formateElapsedTime(countdownTime - elapsedTime);
        }

    }

    private string formateElapsedTime(int seconds)
    {

        // Minutes
        int minutes = seconds > 59 ? (int) Mathf.Floor(seconds / 60.0f) : 0;
        // Secondes
        seconds = seconds > 59 ? seconds - minutes * 60 : seconds;
        
        return (minutes > 9 ? minutes.ToString() : "0" + minutes.ToString()) + ":" + (seconds > 9 ? seconds.ToString() : "0" + seconds.ToString());
    }
}
