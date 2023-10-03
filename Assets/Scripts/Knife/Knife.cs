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
        [HideInInspector] public KnifeTargetable KnifeTargetable;
        [HideInInspector] public KnifeMovement KnifeMovement;
        [HideInInspector] public KnifeCut KnifeCut;

        [Inject]
        public void Init(Controller controller, BakerManager bakerManager)
        {
            _controller = controller;
            _bakerManager = bakerManager;
            BindKnifeTargetable();
            BindKnifeMovement();
            BindKnifeCut();
            KnifeTargetable.Init(KnifeCut);
            KnifeCut.Init(_bakerManager);
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

