using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using System.IO;
using UnityEngine;
using GifEncoder;

[RequireComponent(typeof(Camera))]
public class CameraGifEncoder : MonoBehaviour 
{
	private static readonly	object	_gifLock	= 	new object();

	private static CameraGifEncoder	_instance		=   null;
	private static bool				_hasToCapture	=	false;
	private static bool				_hasToFinish	=	false;

	private AnimatedGifEncoder	_gif				=	null;
	private Queue<GifFrame2D>	_textures			=	new Queue<GifFrame2D> ();
	private Thread				_encodingThread		=	null;

	void Awake ()
	{
		_instance = this;
	}

	public void CreateNewGifEncoder (string path, int frameDelay)
	{
		lock (_gifLock) 
		{
			_textures.Clear ();

			if (_encodingThread != null)
				_encodingThread.Abort ();

			_hasToFinish = false;
			_hasToCapture = false;

			if (_gif != null)
				_gif.Cancel ();
		}

		_gif = new AnimatedGifEncoder (path);
		_gif.SetDelay (frameDelay);		

		_encodingThread	= new Thread (new ThreadStart(WorkThread));
		_encodingThread.Start ();
	}

	void WorkThread ()
	{
		GifFrame2D frame2D 		= null;
		bool	   hasToFinish	= false;

		while (hasToFinish == false)
		{
			lock (_gifLock) 
			{
				if (_textures.Count > 0)
					frame2D = _textures.Dequeue ();

				hasToFinish = _hasToFinish;
			}

			if (frame2D != null) 
			{
				_gif.AddFrame (frame2D);
				frame2D = null;
			}

			Thread.Sleep (10);

			if (hasToFinish == true) 
			{
				_gif.Finish ();
			}
		}
	}

	void OnPostRender ()
	{
		if (_hasToCapture == false)
			return;
		_hasToCapture = false;

		Texture2D tex = new Texture2D (Screen.width, Screen.height);
		tex.ReadPixels (new Rect (0f, 0f, Screen.width, Screen.height), 0,0);
		tex.Apply ();

		lock (_gifLock) 
		{
			_textures.Enqueue (new GifFrame2D (tex));
		}	
	}

	public static void CaptureNewGif (string path, int frameDelay)
	{
		_instance.CreateNewGifEncoder (path, frameDelay);
	}

	public static void CaptureFrame ()
	{
		_hasToCapture = true;
	}

	public static void FinishGif()
	{
		lock (_gifLock) {
			_hasToFinish = true;
		}
	}

	void OnDestroy()
	{
		if (_encodingThread != null)
			_encodingThread.Abort ();

		_instance = null;
	}
}

// Save non accecible data of a Texture2D out the main thread.
public class GifFrame2D
{
	public Color32[]	pixels;
	public int			width;
	public int			height;

	public GifFrame2D(Texture2D tex)
	{
		pixels	= tex.GetPixels32();
		width	= tex.width;
		height	= tex.height;
	}
}