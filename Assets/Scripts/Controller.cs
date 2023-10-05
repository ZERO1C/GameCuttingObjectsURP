using BzKovSoft.ObjectSlicer;
using UnityEngine;
using Zenject;
using Scripts.Knife;

public class Controller : MonoBehaviour
{
    // to do рефактор на три різних класа
    public Transform Katana;
    public AnimationCurve Refinecurve;

    private bool _inProgress;
    private GameObject _slice;
    private Material[] _materials;
    private float _pointY;

    private int _renderFilled = Shader.PropertyToID("_MBDefaultBending");

    private Knife _knife;
    private BakerManager _bakerManager;
    private ObjectForCuttingMovement _objectForCuttingMovement;
    private Vector3 _oldPositionCuttingOb;

    [Inject]
    public void Init(Knife knife, BakerManager bakerManager, ObjectForCuttingMovement objectForCuttingMovement)
    {
        _knife = knife;
        _bakerManager = bakerManager;
        _objectForCuttingMovement = objectForCuttingMovement;
        if (Refinecurve.length == 0) Refinecurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
        _oldPositionCuttingOb = objectForCuttingMovement.transform.position;
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

    public void MoveSlice(Vector3 pos)
    {
        if (!_inProgress)
        {
            return;
        }

        float pointY = pos.y;
        if (_pointY == 0f) _pointY = pointY;
        if (_pointY > pointY)
        {
            _pointY = pointY;
            
        }
        
        float y = Refinecurve.Evaluate(_pointY);
        Vector3 _newPositionCuttingOb = _objectForCuttingMovement.transform.position;
        float sliceThickness = _newPositionCuttingOb.x -_oldPositionCuttingOb.x;   
        sliceThickness = 1f / (sliceThickness*5 + 1f);

        foreach (var material in _materials)
        {
            material.SetFloat(_renderFilled, Mathf.Clamp(y, 0f,10f)*35f* sliceThickness);
        }

        if (_pointY <= 0.1f)
        {
            _slice.GetComponent<Rigidbody>().isKinematic = false;
            _pointY = 0f;
            _inProgress = false;
            _oldPositionCuttingOb = _newPositionCuttingOb;
        }
    }
}
