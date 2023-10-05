using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Scripts.Knife;

namespace Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        private Controller _controller;

        [Inject]
        public void Init(Controller controller)
        {
            _controller = controller;
        } 

        public void MoveControl(bool movementState)
        {
            _controller.MoveControl(movementState);
        }
    }
}

