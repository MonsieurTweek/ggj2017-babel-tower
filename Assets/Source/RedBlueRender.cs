using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RedBlueRender : MonoBehaviour
{
    [SerializeField]
    private Shader _redBlueShader = null;

    [SerializeField]
    private Material _redBlueMaterial = null;

    private Camera _camera = null;


    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnRenderObject()
    {
        _camera.SetReplacementShader(_redBlueShader, string.Empty);
    }

    //private void OnRenderImage(RenderTexture source, RenderTexture destination)
    //{
    //    Graphics.Blit(source, destination, _redBlueMaterial);
    //}
}
