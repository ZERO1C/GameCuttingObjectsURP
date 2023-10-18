using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicer;
using System;

namespace Scripts.Knife
{
    public class KnifeCut : MonoBehaviour
    {
        private BakerManager _bakerManager;
        private GameObject _slice;

        public void Init(BakerManager bakerManager)
        {
            _bakerManager = bakerManager;
        }
        public void Cut(GameObject target)
        {
            var sliceable = target.GetComponent<IBzSliceable>();

            if (sliceable == null)
            {

                return;
            }

            Plane plane = new Plane(Vector3.right, 0f);
            sliceable.Slice(plane, r =>
            {
                if (!r.sliced)
                {
                    return;
                }

                //_pointY = float.MaxValue;
                _slice = r.outObjectPos;
                var meshFilter = _slice.GetComponent<MeshFilter>();
                float centerX = meshFilter.sharedMesh.bounds.center.x;

                StartBaker startBaker = _slice.GetComponent<StartBaker>();
                startBaker.Init(_bakerManager);
                startBaker.StartBakerVoid();
            });
        }


    }
}
