using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void highlightOff(BaseEventData data) {
        GetComponentInChildren<Text>().color = new Color(50f/255f, 50f / 255f, 50f / 255f, 1f);
    }

    public void highlightOn(BaseEventData data) {
        GetComponentInChildren<Text>().color = Color.white;
    }
}
