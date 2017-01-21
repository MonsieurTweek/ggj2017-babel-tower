using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public PlayerIndex playerIndex;

    public Color playerColor;
    public string playerName;
    
    private LinkedList<GameObject> objectsList;
    private float towerHeight;

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
        if(this.hasObject(releasedObject) == false) {
            this.objectsList.AddLast(releasedObject);
            Debug.Log("added");
        }

        this.updateTowerHeight();
       // Debug.Log(this.towerHeight);
    }

    public bool hasObject(GameObject releasedObject) {
        bool found = this.objectsList.Find(releasedObject) != null;
        return found;
    }

    public void removeObject(GameObject releasedObject) {
        if(this.hasObject(releasedObject) == true) {
            this.objectsList.Remove(releasedObject);
            Debug.Log("removed");
        }
    }

    private void updateTowerHeight() {

        float maxFound = float.NaN;

        foreach(GameObject ownedObject in this.objectsList) {
            PolygonCollider2D collider = ownedObject.GetComponent<PolygonCollider2D>();
            for(int i = 0; i < collider.points.Length; i++) {
                
                if(maxFound.Equals(float.NaN)) {
                    maxFound = collider.transform.TransformPoint(collider.points[i]).y;
                } else if(maxFound < collider.transform.TransformPoint(collider.points[i]).y) {
                    maxFound = collider.transform.TransformPoint(collider.points[i]).y;
                }
            }
        }

        this.towerHeight = maxFound;
    }

}

