using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Attach this script to a GameObject to be able to Drag&Drop him.
 **/
public class DragAndDrop : MonoBehaviour {

    // Private attributes
    private bool dragging = false;
    private float distance;

    private void onClick()
    {
        // /!\ TODO : Map the correct button here 
        if (Input.GetMouseButtonDown(0) == true)
        {
            this.distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            this.dragging = true;
        }
    }

    private void onRelease()
    {
        // /!\ TODO : Map the correct button here
        if (Input.GetMouseButtonUp(0) == true)
        {
            this.dragging = false;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.onClick();
        this.onRelease();
        if(this.dragging == true)
        {
            // /!\ TODO : Use the transform of the crosshair instead of the mousePosition
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 rayPoint = ray.GetPoint(this.distance);
            transform.position = rayPoint;
        } 
	}
}
