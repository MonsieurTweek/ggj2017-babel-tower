using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDetection : MonoBehaviour {

    public Game currentGame;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.GetComponent<Rigidbody2D>().velocity.magnitude < 0.02f) { 
            currentGame.triggerGameOver();
        }
    }

}
