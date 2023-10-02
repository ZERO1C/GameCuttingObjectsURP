using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{


    public void MoveKatana(float y)
    {
        Debug.Log(1 / _pointY);

        var pos = transform.position;
        pos.y = y;
        transform.position = pos;

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
            material.SetFloat(_renderFilled, 1 / _pointY * 5);
        }

        if (y <= 0)
        {
            _slice.GetComponent<Rigidbody>().isKinematic = false;
            //Destroy(_slice.transform.parent.gameObject);
            _inProgress = false;
        }
    }
}
