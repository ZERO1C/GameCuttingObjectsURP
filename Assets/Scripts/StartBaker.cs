using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StartBaker : MonoBehaviour
{
    BakerManager _bakerManager;
    Renderer[] renderers;
    GameObject[] gameObjects;

    public void Init(BakerManager bakerManager)
    {
        _bakerManager = bakerManager;
    }

    public void StartBakerVoid()
    {
        renderers  = GetComponentsInChildren<Renderer>();
        gameObjects = new GameObject[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            gameObjects[i] = renderers[i].gameObject;
        }
        _bakerManager.BakeObjects(gameObjects);
    }
}
