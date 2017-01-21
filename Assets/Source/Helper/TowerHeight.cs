using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Detect the highest shape of the world => to improve, just the general idea here for now
 **/
public class TowerHeight : MonoBehaviour {

    private LinkedList<PolygonCollider2D> spriteList = new LinkedList<PolygonCollider2D>();

	// Use this for initialization
	void Start () {
        // get all GO with tag "testTag" (for test purpose)
        GameObject[] testTagsObjects = GameObject.FindGameObjectsWithTag("testTag");

        // get their polygonCollider and add it to the linked list
        for (int i = 0; i < testTagsObjects.Length; i++)
        {
            spriteList.AddLast(testTagsObjects[i].GetComponent<PolygonCollider2D>());
        }

        this.getTopSprite();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void getTopSprite()
    {

        // WARNING : Min height to define !
        float yMax = -50f;

        // Iterate over the linked list of PolygonCollider
        foreach (PolygonCollider2D spriteCollider in spriteList)
        {
            // get points of the polygonCollider
            Vector2[] points = spriteCollider.points;

            // find the highest point
            for(int i = 0; i < points.Length; i++)
            {
                if (spriteCollider.transform.TransformPoint(points[i]).y > yMax)
                {
                    yMax = spriteCollider.transform.TransformPoint(points[i]).y;
                }
            }
        }
        Debug.Log(yMax);

    }
}
