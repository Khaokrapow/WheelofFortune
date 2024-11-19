using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoard : MonoBehaviour
{
    public float boost;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            GameObject parent = GameObject.FindGameObjectWithTag("Player");
            Rigidbody rb = parent.GetComponent<Rigidbody>();
            //Debug.Log(rb.mass);
            var force = parent.transform.forward * boost;
            Debug.Log(force);
            rb.AddForce(force, ForceMode.Acceleration);


        }
    }
}
