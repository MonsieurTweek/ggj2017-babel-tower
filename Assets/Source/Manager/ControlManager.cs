using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour
{

	private Player _player = null;

	// Use this for initialization
	public void Start ()
	{
		_player = Game.instance.player;	
	}
	
	// Update is called once per frame
	public void Update ()
	{

	}
}

