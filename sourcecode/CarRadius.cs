using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRadius : MonoBehaviour
{
    public bool isInsideRadiusSphere = false;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            isInsideRadiusSphere = true;
            //Debug.Log("stay");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInsideRadiusSphere = false;
            //Debug.Log("not stay");
        }
    }

}
