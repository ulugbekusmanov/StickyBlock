using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SphereObj"))
        {
            other.transform.parent.GetComponent<CollectedObjController>().DropObj();
        }
        
    }
}
