using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game instance;
    public Player[] players;

	private Timer _Timer = new Timer(0f);
    private Timer _HurryUpTimer = new Timer(0f);
    private float elapsedTime = 0f; // in seconds
    public float countdownTime = 120f; // in seconds
    public AnimationCurve animationCurve = null;

    // UI Elements
    public CountdownAnimator countdown;
    private Text countdownLabel;
    public GameObject victoryScreen;
	public DangerWarning dangerWarning;

    // Engine
    public SpawnBlock spawnEngine;

	public bool dangerActivated = true;

	public EffectBlock alienEffect = null;
	public EffectBlock	tsunamiEffect = null;
	public EffectBlock	windEffect = null;
	public EffectBlock 	quakeEffect = null;

	private DangerEngine _dangerEngine = null;

	public void Awake(){
		instance = this;
#if UNITY_EDITOR
		Application.targetFrameRate = 60;
#endif
	}


	// Use this for initialization
	public void Start () {
        countdownLabel = countdown.GetComponentInChildren<Text>();
        countdownLabel.text = formateElapsedTime(countdownTime);
        _Timer.SetDuration(1.0f);
        _Timer.Start();

		_dangerEngine  = new DangerEngine();

		AudioManager.instance.mainMusic.Play ();
    }
	
	// Update is called once per frame
	public void Update () {

		if (dangerActivated == true)
        	_dangerEngine.Update();
		
		if( _Timer.isFinished() == true)
        {
            elapsedTime += 1f;
            _Timer.SetDuration(1f);
            _Timer.Start();

            countdownLabel.text = formateElapsedTime(countdownTime - elapsedTime);

            if (countdown.hurryUp == false && countdownTime - elapsedTime < 10f)
            {
                countdown.hurryUp = true;
            }

            if (elapsedTime >= countdownTime)
            {
                Debug.Log("Game Over : End of time !");
                triggerGameOver();
            }
        }

    }

    private string formateElapsedTime(float seconds)
    {
        // Minutes
        int minutes = seconds > 59 ? (int) Mathf.Floor(seconds / 60.0f) : 0;
        // Secondes
        seconds = seconds > 59 ? seconds - minutes * 60f : seconds;
        
        return (minutes > 9 ? minutes.ToString() : "0" + minutes.ToString()) + ":" + (seconds > 9f ? seconds.ToString() : "0" + seconds.ToString());
    }

    public void triggerGameOver()
    {
        _Timer.Stop();

        Player winner = players[0];
        for(int i = 0; i < players.Length; i++)
        {
            if(winner.towerHeight < players[i].towerHeight)
            {
                winner = players[i];
            }
        }

        // Disable spawn
        spawnEngine.spawnActivated = false;

        // Show victory screen
        Image[] images = victoryScreen.GetComponentsInChildren<Image>();
        for(int i = 0; i < images.Length; i++)
        {
            images[i].color = winner.playerColor;
        }
        Text victoryLabel = victoryScreen.GetComponentInChildren<Text>();
        victoryLabel.color = winner.playerColor;
        victoryLabel.text = (winner.playerName + " wins").ToUpper();
        victoryScreen.SetActive(true);
    }
}
