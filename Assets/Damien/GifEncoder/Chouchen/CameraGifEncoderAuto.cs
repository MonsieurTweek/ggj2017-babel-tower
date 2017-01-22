using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[RequireComponent(typeof(CameraGifEncoder))]
public class CameraGifEncoderAuto : MonoBehaviour 
{
	public bool		autoStart			=   false;
	public bool		captureByFrameCount	=	false;
	public int		frameCountNumber	=	0;

	public string	folderPath			=	"";
	public string	gameName			=	"";
	public int		frameDelay			=	0;

	private int 	_frameCount 		=	0;

	public void Start ()
	{
		if (autoStart == true) 
			CameraGifEncoder.CaptureNewGif (GetPath (), frameDelay);		
	}

	public void Update ()
	{
		if (captureByFrameCount == true) 
		{
			_frameCount++;

			if (_frameCount == frameCountNumber)
			{
				CameraGifEncoder.CaptureFrame ();
				_frameCount = 0;
			}
		}

		if (Input.GetKeyDown (KeyCode.G)) 
		{
			CameraGifEncoder.FinishGif();
		}
	}

	public string GetPath ()
	{
		DateTime now = DateTime.Now;

		string name = now.Year	 + "" +
					  now.Month  + "" +
					  now.Day 	 + "" +
					  now.Hour	 + "" +
					  now.Minute + "" +
					  now.Second;

		string path	= folderPath + Path.DirectorySeparatorChar +
					  gameName	 + "-" +
					 name	     + ".gif"; 	

		return path;
	}
}
