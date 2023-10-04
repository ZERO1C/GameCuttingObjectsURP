using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Knife
{
    public class KnifeMovement : MonoBehaviour
    {
        public float SpeedMove = 1;

        private Controller _controller;
        private ObjectForCuttingMovement _objectForCuttingMovement;
        private MovementStateKnife _state = MovementStateKnife.Stand;
        private Vector3 _startPosition;
        private bool _sliceCutOff;

        public void Init(Controller controller, ObjectForCuttingMovement objectForCuttingMovement)
        {
            _objectForCuttingMovement = objectForCuttingMovement;
            _startPosition = transform.position;
            _controller = controller;
            StartCoroutine(MoveKnife());
        }
        
        public void SetState(MovementStateKnife state)
        {
            switch (state)
            {
                case MovementStateKnife.Up:
                    break;
                case MovementStateKnife.Down:
                    break;     
                case MovementStateKnife.Stand:
                    break;   
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _state = state;
        }
        IEnumerator MoveKnife()
        {
            while (true)
            {
                switch (_state)
                {
                    case MovementStateKnife.Up:
                        MoveKnife(Vector3.up);
                        break;
                    case MovementStateKnife.Down:
                        MoveKnife(Vector3.down);
                        break;
                    case MovementStateKnife.Stand:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return new WaitForFixedUpdate();
            }

        }
        public void MoveKnife(Vector3 vector)
        {
            var pos = transform.position;
            pos += vector * SpeedMove / 100;
            pos.y = Mathf.Clamp(pos.y, 0, _startPosition.y);
            transform.position = pos;
            _controller.MoveKnife(pos);
            if (!_sliceCutOff && pos.y == 0) _sliceCutOff = true;
            else if (_sliceCutOff && pos.y == _startPosition.y)
            {
                _objectForCuttingMovement.SetState(ObjectForCuttingMovement.MovementStateObject.Forward);
                _sliceCutOff = false;
            }
        }
    }
    public enum MovementStateKnife
    {
        Up,
        Down,
        Stand
    }
    
    
}