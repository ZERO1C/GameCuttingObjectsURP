using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class BakerManager : MonoBehaviour
{
    private MB3_MeshBaker _meshBaker;
    private Controller _controller;

    [Inject]
    public void Init(Controller controller)
    {
        _controller = controller;
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
        _controller.BindNewSlice(GetComponentInChildren<Renderer>().gameObject);
    }

}
