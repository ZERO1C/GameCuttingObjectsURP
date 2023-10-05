using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StartBaker : MonoBehaviour
{
    private BakerManager _bakerManager;
    private Renderer[] _renderers;
    private GameObject[] _gameObjects;

    public void Init(BakerManager bakerManager)
    {
        _bakerManager = bakerManager;
    }

    public void StartBakerVoid()
    {
        _renderers  = GetComponentsInChildren<Renderer>();
        _gameObjects = new GameObject[_renderers.Length];
        for (int i = 0; i < _renderers.Length; i++)
        {
            _gameObjects[i] = _renderers[i].gameObject;
        }
        _bakerManager.BakeObjects(_gameObjects);
    }
}
