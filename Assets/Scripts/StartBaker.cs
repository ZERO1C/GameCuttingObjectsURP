using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBaker : MonoBehaviour
{
    public void StartBakerVoid()
    {
        Renderer[] renderers  = GetComponentsInChildren<Renderer>();
        GameObject[] gameObjects = new GameObject[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            gameObjects[i] = renderers[i].gameObject;
        }
        Debug.Log(546);
        BakerManager.Baker?.BakeObjects(gameObjects);
    }
}
