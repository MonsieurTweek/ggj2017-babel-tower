using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Woosh : MonoBehaviour {

    public AnimationCurve curve;
    public Text countdown;
    private float _elapsedTime;
    private bool _processAnim = true;    

	// Use this for initialization
	void Start () {
        _elapsedTime = 0;

		AudioManager.instance.waterDrop.PlayDelayed (0.1f);
		AudioManager.instance.waterDrop2.PlayDelayed (1.1f);
		AudioManager.instance.waterDrop3.PlayDelayed (2.1f);
	}
	
	// Update is called once per frame
	void Update () {
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime < 3f)
        {
            countdown.transform.localScale = Vector3.one * curve.Evaluate(_elapsedTime);
            countdown.text = (3f - Mathf.Floor(_elapsedTime)).ToString();
        } else if(_processAnim == true)
        {
            GetComponent<Animator>().Stop();
            _processAnim = false;
            Game.instance.enableInputs();

			AudioManager.instance.mainMusic.Play ();
        }
    }

    public void Reset()
    {
        _elapsedTime = 0;
        _processAnim = true;
        GetComponent<Animator>().Play("321Animation");
    }
}
