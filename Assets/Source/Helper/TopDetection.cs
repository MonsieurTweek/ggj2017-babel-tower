using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.GetComponent<Rigidbody2D>().velocity == Vector2.zero) {
            Debug.Log("GAME OVER");
        }
    }

}
