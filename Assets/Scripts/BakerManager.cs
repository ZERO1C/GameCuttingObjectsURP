using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakerManager : MonoBehaviour
{

    public static BakerManager Baker;
    private MB3_MeshBaker _meshBaker;
    private void Awake()
    {
        Baker = this;
        _meshBaker = GetComponent<MB3_MeshBaker>();
    }
    public void BakeObjects(GameObject[] gos)
    {
        if (gos.Length > 0)
        {
            _meshBaker.ClearMesh();
            _meshBaker.AddDeleteGameObjects(gos, gos, disableRendererInSource: true);
            _meshBaker.Apply();
            StartCoroutine(NewSlice());
        }

    }
    IEnumerator NewSlice()
    {
        yield return null;
        GameManager.Instance.BakeSlice(GetComponentInChildren<Renderer>().gameObject);
    }

}
