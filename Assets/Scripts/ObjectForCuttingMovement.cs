using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectForCuttingMovement: MonoBehaviour
{
    public float SpeedMove=1;
    private MovementStateObject _state;

    [Inject]
    public void Init()
    {
        StartCoroutine(MoveObject());
    }
    public void SetState(MovementStateObject state)
    {
        switch (state)
        {
            case MovementStateObject.Forward:
                break;
            case MovementStateObject.Stand:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        _state = state;
    }
    IEnumerator MoveObject()
    {
        
        while (true)
        {
            switch (_state)
            {
                case MovementStateObject.Forward:
                    MoveObject(Vector3.right);
                    break;
                case MovementStateObject.Stand:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            yield return new WaitForFixedUpdate();
        }

    }
    public void MoveObject(Vector3 vector)
    {
        var pos = transform.position;
        pos += vector * SpeedMove / 100;
        transform.position = pos;
    }
    public enum MovementStateObject
    {
        Forward,
        Stand

    }
}
