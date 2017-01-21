using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public static Game instance;
    public Player[] players;

	private Timer _Timer = new Timer(0f);
    private int elapsedTime = 0; // in seconds
    public int countdownTime = 120; // in seconds

    // UI Elements
    public GameObject countdown;
    private Text countdownLabel;
    public GameObject victoryScreen;

    // Engine
    public SpawnBlock spawnEngine;

	public bool dangerActivated = true;

	public AlienEffect alienEffect = null;
	public WindEffect	tsunamiEffect = null;
	public WindEffect	windEffect = null;
	public QuakeEffect 	quakeEffect = null;

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

		if (dangerActivated == true)
        	_dangerEngine.Update();
		
		if(_Timer.isFinished() == true)
        {
            elapsedTime++;
            countdownLabel.text = formateElapsedTime(countdownTime - elapsedTime);

            if(elapsedTime == countdownTime)
            {
                Debug.Log("Game Over : End of time !");
                triggerGameOver();
            } else
            {
                _Timer.SetDuration(1.0f);
                _Timer.Start();
            }
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
