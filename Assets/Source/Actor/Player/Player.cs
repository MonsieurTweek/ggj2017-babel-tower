using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public PlayerIndex playerIndex;

    
    private LinkedList<GameObject> objectsList;

    // Use this for initialization
    public void Start ()
	{
        this.objectsList = new LinkedList<GameObject>();
    }

	// Update is called once per frame
	void Update ()
	{

	}

    public void addObject(GameObject releasedObject) {
        this.objectsList.AddLast(releasedObject);
    }

}

