
using BzKovSoft.ObjectSlicer;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    bool _inProgress;
    GameObject _slice;
    Material[] _materials;
    float _pointY;

    public Transform Box;
    public Transform Katana;
    int _renderFilled = Shader.PropertyToID("_MBDefaultBending");
    public void Cut(GameObject target)
    {
        var sliceable = target.GetComponent<IBzSliceable>();
        if (sliceable == null)
        {
            return;
        }

        Plane plane = new Plane(Vector3.right, 0f);
        sliceable.Slice(plane, r =>
        {
            if (!r.sliced)
            {
                return;
            }

            _inProgress = true;
            _pointY = float.MaxValue;
            _slice = r.outObjectPos;
            var meshFilter = _slice.GetComponent<MeshFilter>();
            float centerX = meshFilter.sharedMesh.bounds.center.x;

            _materials = _slice.GetComponent<MeshRenderer>().materials;
            _slice.GetComponent<StartBaker>().StartBakerVoid();

            

            //foreach (var material in _materials)
            //{
            //    material.SetFloat("_PointX", centerX);
            //}
        });
    }
    public void BakeSlice( GameObject slice)
    {
        if (slice)
        {
            _slice = slice;
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


    }

    public GameManager()
    {
        Instance = this;
    }

    public void MoveBox(float x)
    {
        var pos = Box.position;
        pos.x = x;
        Box.position = pos;
    }

    public void MoveKatana(float y)
    {
        Debug.Log(1 / _pointY);

        var pos = Katana.position;
        pos.y = y;
        Katana.position = pos;

        if (!_inProgress)
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
            material.SetFloat(_renderFilled, 1/ _pointY*5);
        }

        if (y <= 0)
        {
            _slice.GetComponent<Rigidbody>().isKinematic = false;
            //Destroy(_slice.transform.parent.gameObject);
            _inProgress = false;
        }
    }
}
