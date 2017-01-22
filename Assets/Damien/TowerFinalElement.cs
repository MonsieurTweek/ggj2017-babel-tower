using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFinalElement : MonoBehaviour 
{
	private SpriteRenderer _spriteRenderer = null;
	public BlockFamily family;

	public void ColorSprite(Color playerColor, Texture2D atlas)
	{
		_spriteRenderer = GetComponent<SpriteRenderer> ();

		Color color = new Color(0f, 0f, 0f, 0f);
		switch(family)
		{
		case BlockFamily.Cosmos:
			color = new Color(1f, 0f, 0f, 0f);
			break;

		case BlockFamily.Tsunami:
			color = new Color(0f, 1f, 0f, 0f);
			break;

		case BlockFamily.Wind:
			color = new Color(0f, 0f, 1f, 0f);
			break;

		case BlockFamily.Quake:
			color = new Color(0f, 0f, 0f, 1f);
			break;

		}

		_spriteRenderer.color = playerColor;
		_spriteRenderer.material.SetColor ("_BlockType", color);
		_spriteRenderer.material.SetTexture ("_BlockAtlas", atlas);	
	}
}
