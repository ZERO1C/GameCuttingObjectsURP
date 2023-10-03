using BzKovSoft.ObjectSlicer;
using UnityEngine;
using Zenject;
using Scripts.Knife;

public class Controller : MonoBehaviour
{

    public Transform Box;
    public Transform Katana;

    private bool _inProgress;
    private GameObject _slice;
    private Material[] _materials;
    private float _pointY;

    private int _renderFilled = Shader.PropertyToID("_MBDefaultBending");

    private Knife _knife;
    private BakerManager _bakerManager;

    [Inject]
    public void Init(Knife knife, BakerManager bakerManager)
    {
        _knife = knife;
        _bakerManager = bakerManager;
    }
    
    public void BindNewSlice( GameObject slice)
    {
        _slice = slice;
        _materials = _slice.GetComponent<MeshRenderer>().materials;
        Rigidbody rb = _slice.GetComponent<Rigidbody>();
        if (!rb)
        {
            rb = _slice.AddComponent<Rigidbody>();
            _slice.AddComponent<MeshCollider>().convex = true;
            rb.useGravity = true;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        rb.isKinematic = true;
        _slice.transform.localRotation = Quaternion.identity;
    }

    public void MoveBox(float x)
    {
        var pos = Box.position;
        pos.x = x;
        Box.position = pos;
    }

    public void MoveKnife(float y)
    {
        Debug.Log(y);
        var pos = _knife.KnifeMovement.MoveKnife(y);

        if (!_slice)
        {
            return;
        }

        float pointY = _slice.transform.InverseTransformPoint(pos).y;
        if (_pointY > pointY)
        {
            _pointY = pointY;
        }

        foreach (var material in _materials)
        {
            material.SetFloat(_renderFilled, 1/ Mathf.Clamp(y,0.1f,10)*5);
        }

        if (y <= 0)
        {
            _slice.GetComponent<Rigidbody>().isKinematic = false;
            //Destroy(_slice.transform.parent.gameObject);
            _inProgress = false;
        }
    }
}
