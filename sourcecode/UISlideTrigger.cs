using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlideTrigger : MonoBehaviour
{
    
    public GameObject panel;
    private Animator animator ;

    void Start()
    {
        // Ensure the Animator is properly assigned
        
        animator = panel.GetComponent<Animator>();
    }

    // This method should be called to slide the UI in
    public void HitGenerateBlock()
    {
        
        if (animator != null)
        {
            // Set the boolean to trigger slide-in animation
            animator.SetBool("IsNowIn", true);
            Debug.Log("Play slide in");
        }
    }

    // This method should be called to slide the UI out
    public void HitAnswer()
    {
        
        if (animator != null)
        {
            // Set the boolean to trigger slide-out animation
            animator.SetBool("IsNowIn", false);
            Debug.Log("Play slide out");
        }
    }
}
