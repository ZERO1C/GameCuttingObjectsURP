using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        ObjectForCuttingMovement objectCutting = other.GetComponent<ObjectForCuttingMovement>();
        if (objectCutting)
        {
            objectCutting.DropObject();
        }
    }
}
