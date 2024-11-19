using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
        // Get the Renderer component of the GameObject
        Renderer renderer = GetComponent<Renderer>();

        // Check if the Renderer component is attached to the GameObject
        if (renderer != null)
        {
            // Set the Renderer component's enabled property to false
            renderer.enabled = false;
        }
        else
        {
            Debug.LogWarning("Renderer component not found on this GameObject.");
        }
    }

}
