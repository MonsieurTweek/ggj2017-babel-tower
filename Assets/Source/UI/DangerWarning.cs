using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DangerWarning : MonoBehaviour {

	public AnimationCurve animationCurve = null;

	private Timer _timer = new Timer(0f);
	private Timer _loopTimer = new Timer(0f);

	public Image image = null;
    public GameObject go1 = null;
    public GameObject go2 = null;

    public float loopDuration = 0f;

	public Sprite windSprite;
	public Sprite tsunamiSprite;
	public Sprite alienSprite;
	public Sprite quakeSprite;

    public List<GameObject> arrowGo = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
        go1.SetActive(false);
        go2.SetActive(false);
        for(int i = 0; i < arrowGo.Count; i++)
        {
            arrowGo[i].SetActive(false);
        }
    }
		
	public void SetDanger(BlockFamily blocFamily, float duration)
	{
        go1.SetActive(true);
        go2.SetActive(true);

        _timer.SetDuration (duration);
		_timer.Start ();

		_loopTimer.SetDuration (loopDuration);
		_loopTimer.Start ();

		switch (blocFamily) 
		{
		case BlockFamily.Cosmos:
			image.sprite = alienSprite;
            arrowGo[0].SetActive(true);
			break;
		case BlockFamily.Tsunami:
			image.sprite = tsunamiSprite;
            arrowGo[1].SetActive(true);
            break;
		case BlockFamily.Wind:
			image.sprite = windSprite;
            arrowGo[3].SetActive(true);
            break;
		case BlockFamily.Quake:
			image.sprite = quakeSprite;
            arrowGo[2].SetActive(true);
            break;
		default:
			break;
		}
	}

	public void Hide()
	{
        go1.SetActive(false);
        go2.SetActive(false);
        for (int i = 0; i < arrowGo.Count; i++)
        {
            arrowGo[i].SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (_timer.IsRunning () == true) 
		{
			if (_loopTimer.isFinished() == true)
				_loopTimer.Start ();

			//transform.localScale = Vector3.one *  animationCurve.Evaluate (_loopTimer.GetCurrentTime () / _loopTimer.Duration);
		}

		
	}
}
