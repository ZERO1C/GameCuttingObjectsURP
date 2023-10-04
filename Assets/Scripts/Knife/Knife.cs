using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Scripts.Knife
{
    public class Knife : MonoBehaviour
    {
        private Controller _controller;
        private BakerManager _bakerManager;
        private ObjectForCuttingMovement _objectForCuttingMovement;

        [HideInInspector] public KnifeTargetable KnifeTargetable;
        [HideInInspector] public KnifeMovement KnifeMovement;
        [HideInInspector] public KnifeCut KnifeCut;
        
        [Inject]
        public void Init(Controller controller, BakerManager bakerManager, ObjectForCuttingMovement objectForCuttingMovement)
        {
            _controller = controller;
            _bakerManager = bakerManager;
            _objectForCuttingMovement = objectForCuttingMovement;
            BindKnifeTargetable();
            BindKnifeMovement();
            BindKnifeCut();
            KnifeTargetable.Init(KnifeCut);
            KnifeCut.Init(_bakerManager);
            KnifeMovement.Init(_controller, _objectForCuttingMovement);
        }
        public void MovementSetState(MovementStateKnife state)
        {
            KnifeMovement.SetState(state);
        }
        private void BindKnifeTargetable()
        {
            if (!KnifeTargetable) KnifeTargetable = GetComponent<KnifeTargetable>();
        }     
        private void BindKnifeMovement()
        {
            if (!KnifeMovement) KnifeMovement = GetComponent<KnifeMovement>();
        }
        private void BindKnifeCut()
        {
            if (!KnifeCut) KnifeCut = GetComponent<KnifeCut>();
        }
    }
}

