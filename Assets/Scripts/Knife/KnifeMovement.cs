using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Knife
{
    public class KnifeMovement : MonoBehaviour
    {
        private int _pointY;

        public Vector3 MoveKnife(float y)
        {

            var pos = transform.position;
            pos.y = y;
            transform.position = pos;
            return pos;
        }
    }
}