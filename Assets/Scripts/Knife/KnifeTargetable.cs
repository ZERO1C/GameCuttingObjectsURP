using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Knife
{
    public class KnifeTargetable : MonoBehaviour
    {
        private KnifeCut _knifeCut;

        public void Init(KnifeCut knifeCut)
        {
            _knifeCut = knifeCut;
        }
        private void OnTriggerEnter(Collider other)
        {
            _knifeCut.Cut(other.gameObject);
        }
    }
}

