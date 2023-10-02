using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Knife
{
    public class KnifeTargetable : MonoBehaviour
    {
        private GameManager _gameManager;

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        private void OnTriggerEnter(Collider other)
        {
            _gameManager.Cut(other.gameObject);
        }
    }
}

