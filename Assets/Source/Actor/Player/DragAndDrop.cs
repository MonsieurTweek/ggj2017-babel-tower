using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    // Private attributes
    private bool dragging = false;
    private float distance;
    private GameObject target;
    private Vector3 screenPosition;
    private Vector3 offset;

    private void onClick()
    {

        // /!\ TODO : Map the correct button here 
        if (Input.GetMouseButtonDown(0) == true)
        {
            RaycastHit hitInfo;
           this.target = ReturnClickedObject(out hitInfo);
            if (this.target != null)
            {
                this.dragging = true;

                //Convert world position to screen position.
                this.screenPosition = Camera.main.WorldToScreenPoint(this.target.transform.position);
                this.offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.screenPosition.z));
            }
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

        if (this.dragging)
        {
            //track mouse position.
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.screenPosition.z);

            //convert screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + this.offset;

            //It will update target gameobject's current postion.
            this.target.transform.position = currentPosition;
        }
    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        // /!\ TODO : Use the transform of the crosshair instead of the mousePosition
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
}
