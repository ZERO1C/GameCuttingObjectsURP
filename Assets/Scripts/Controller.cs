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
    private ObjectForCuttingMovement _objectForCuttingMovement;

    [Inject]
    public void Init(Knife knife, BakerManager bakerManager, ObjectForCuttingMovement objectForCuttingMovement)
    {
        _knife = knife;
        _bakerManager = bakerManager;
        _objectForCuttingMovement = objectForCuttingMovement;
    }
    
    public void MoveControl(bool movementState)
    {
        if (movementState)
        {
            _knife.MovementSetState(MovementStateKnife.Down);
            _objectForCuttingMovement.SetState(ObjectForCuttingMovement.MovementStateObject.Stand);
        }
        else
        {
            _knife.MovementSetState(MovementStateKnife.Up);

        }

    }


    public void BindNewSlice( GameObject slice)
    {
        _slice = slice;
        _materials = _slice.GetComponent<MeshRenderer>().materials;
        Rigidbody rb = _slice.GetComponent<Rigidbody>();
        if (!rb)
        {
            rb = _slice.AddComponent<Rigidbody>();
            //_slice.AddComponent<MeshCollider>().convex = true;
            rb.useGravity = true;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        rb.isKinematic = true;
        _slice.transform.localRotation = Quaternion.identity;
        var pos = _slice.transform.position;
        pos.y = -0.2f;
        _slice.transform.position = pos;
        _inProgress = true;
    }

    public void MoveBox(float x)
    {
        var pos = Box.position;
        pos.x = x;
        Box.position = pos;
    }

    public void MoveKnife(Vector3 pos)
    {
        if (!_inProgress)
        {
            return;
        }

        float pointY = pos.y;
        if (_pointY == 0) _pointY = pointY;
        if (_pointY > pointY)
        {
            _pointY = pointY;
        }
        Debug.Log(_pointY);
        foreach (var material in _materials)
        {
            material.SetFloat(_renderFilled, 1/ Mathf.Clamp(_pointY, 0.1f,10)*3);
        }

        if (_pointY <= 0.1f)
        {
            _slice.GetComponent<Rigidbody>().isKinematic = false;
            _pointY = 0;
            _inProgress = false;
        }
    }
}
