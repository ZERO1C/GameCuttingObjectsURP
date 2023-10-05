using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectForCuttingMovement: MonoBehaviour
{
    // to do ініціалізація з фабрикі коли появиться фабрика
    public float SpeedMove=1f;
    private MovementStateObject _state;
    private Rigidbody _rb;

    [Inject]
    public void Init()
    {
        _rb = GetComponent<Rigidbody>();
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
    private IEnumerator MoveObject()
    {
        
        while (true)
        {
            switch (_state)
            {
                case MovementStateObject.Forward:
                    MoveObject(Vector3.right* SpeedMove/10f);
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
        pos += vector * SpeedMove / 100f;
        transform.position = pos;
    }
    
    public void DropObject()
    {
        SetState(MovementStateObject.Stand);
        _rb.isKinematic = false;
        Destroy(gameObject, 3f);
    }
}
