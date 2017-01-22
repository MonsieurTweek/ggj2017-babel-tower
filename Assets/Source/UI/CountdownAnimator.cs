using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownAnimator : MonoBehaviour
{

    public AnimationCurve curve;
    public Text countdown;
    private float _elapsedTime;
    public bool hurryUp = true;

    // Use this for initialization
    void Start()
    {
        _elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(hurryUp == true)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime < 10f)
            {
                countdown.transform.localScale = Vector3.one * curve.Evaluate(_elapsedTime);
            }
            else if (hurryUp == true)
            {
                hurryUp = false;
            }
        }
    }

    public void Reset()
    {
        _elapsedTime = 0;
        hurryUp = false;
    }
}
