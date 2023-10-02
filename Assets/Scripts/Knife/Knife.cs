using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Scripts.Knife
{
    public class Knife : MonoBehaviour
    {
        private GameManager _gameManager;
        [HideInInspector] public KnifeTargetable KatanaTargetable;
        [HideInInspector] public KnifeMovement KatanaMovement;

        [Inject]
        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
            BindKatanaTargetable();
            BindKatanaMovement();
            KatanaTargetable.Init(_gameManager);
        }
        private void BindKatanaTargetable()
        {
            if (!KatanaTargetable) KatanaTargetable = GetComponent<KnifeTargetable>();
        }     
        private void BindKatanaMovement()
        {
            if (!KatanaMovement) KatanaMovement = GetComponent<KnifeMovement>();
        }
    }
}

