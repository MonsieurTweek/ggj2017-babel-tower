using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFinal : MonoBehaviour 
{
	public Texture2D atlas;

	public Transform[] levelRoots = null;

	public float duration = 0f;

	public AnimationCurve spawnCurve = null;



	// Use this for initialization
	void Start () {
		Win (2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Win(int level)
	{
		for (int i = 0; i < levelRoots.Length; i++) 
		{
			if (i != level)
			{
				levelRoots [i].gameObject.SetActive (false);
			}
			else 
			{
				TowerFinalElement[] elements = levelRoots [i].transform.GetComponentsInChildren<TowerFinalElement> ();

				levelRoots [i].gameObject.SetActive (true);

				foreach (TowerFinalElement element in elements)
					element.ColorSprite (Color.red, atlas);

				StartCoroutine(SpawnTower(levelRoots[i]));
			}
		}
	}

	IEnumerator SpawnTower(Transform root)
	{
		Transform[] stairs = new Transform[root.childCount];
		for (int i = 0; i < root.childCount; i++)
			stairs[i] =	root.GetChild (i);
	
		for (int i = 0; i < stairs.Length; i++)
			stairs [i].localScale = new Vector3 (1f, 0f, 1f);

		for (int i = 0; i < stairs.Length; i++) 
		{
			float startTime = Time.time;
			float t = 0f;

			while (t < 1f) 
			{
				t = (Time.time - startTime) / duration;

				float lerpT = spawnCurve.Evaluate (t);

				float scaleY = Mathf.Lerp (0f, 1f, lerpT);

				stairs [i].localScale = new Vector3 (1f, scaleY, 1f);

				yield return null;
			}

			yield return new WaitForSeconds (0.1f);
		}
	}
}
