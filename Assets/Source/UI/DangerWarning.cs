using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DangerWarning : MonoBehaviour {

	public AnimationCurve animationCurve = null;

	private Timer _timer = new Timer(0f);
	private Timer	_loopTimer = new Timer(0f);

	public Image image = null;

	public float loopDuration = 0f;

	public Sprite windSprite;
	public Sprite tsunamiSprite;
	public Sprite alienSprite;
	public Sprite quakeSprite;

	// Use this for initialization
	void Start () 
	{
		
	}
		
	public void SetDanger(BlockFamily blocFamily, float duration)
	{
		image.enabled = true;

		_timer.SetDuration (duration);
		_timer.Start ();

		_loopTimer.SetDuration (loopDuration);
		_loopTimer.Start ();

		switch (blocFamily) 
		{
		case BlockFamily.Cosmos:
			image.sprite = alienSprite;
			break;
		case BlockFamily.Tsunami:
			image.sprite = tsunamiSprite;
			break;
		case BlockFamily.Wind:
			image.sprite = windSprite;
			break;
		case BlockFamily.Quake:
			image.sprite = quakeSprite;
			break;
		default:
			break;
		}
	}

	public void Hide()
	{
		image.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_timer.IsRunning () == true) 
		{
			if (_loopTimer.isFinished() == true)
				_loopTimer.Start ();

			transform.localScale = Vector3.one *  animationCurve.Evaluate (_loopTimer.GetCurrentTime () / _loopTimer.Duration);
		}

		
	}
}
