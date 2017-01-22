using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public PlayerIndex playerIndex;

    public Color playerColor;
    public string playerName;
    
    private LinkedList<GameObject> objectsList;
    public float towerHeight;

    // UI Score
    public Text score;

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
            GameObject.Find("Spawn Area").GetComponent<SpawnBlock>().blockList.Remove(releasedObject);
            releasedObject.GetComponentInChildren<SpriteRenderer>().color = playerColor;
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
            GameObject.Find("Spawn Area").GetComponent<SpawnBlock>().addBlock(releasedObject);
            releasedObject.GetComponentInChildren<SpriteRenderer>().color = new Color(50f/255f, 50f/255f, 50f/255f);
            releasedObject.transform.position = new Vector3(releasedObject.transform.position.x, releasedObject.transform.position.y, 0.0f);
        }
        this.updateTowerHeight();
    }

    private void updateTowerHeight() {

        float maxFound = -2.5f;

        foreach(GameObject ownedObject in this.objectsList)
		{
            PolygonCollider2D collider = ownedObject.GetComponent<PolygonCollider2D>();

			Vector2[] points = collider.points;

			Vector2 higherPoint = new Vector2 (float.MinValue, float.MinValue);

			for (int i = 0; i < points.Length; i++) 
				if (points [i].y > higherPoint.y)
					higherPoint = points [i];

			Vector3 worldPoint = collider.transform.TransformPoint (higherPoint);

			maxFound = Mathf.Max (maxFound, worldPoint.y);
        }

        this.towerHeight = maxFound;

        updateScore();
    }

    private void updateScore()
    {
        if(towerHeight + 2.5f > 0)
        {
            score.text = (towerHeight + 2.5f).ToString("F1") + "m";
        } else
        {
            score.text = "0.0m";
        }
    }

}

