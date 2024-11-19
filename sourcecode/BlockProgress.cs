using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BlockProgress : MonoBehaviour
{
    public float percent = 10f; // กำหนดเปอร์เซ็นต์การเพิ่มใน Inspector
    private ProgressTracker progressTracker;

    void Start()
    {
        progressTracker = FindObjectOfType<ProgressTracker>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ป้องกันการย้อนกลับบางส่วน
            if (progressTracker.currentProgress <= 10 && percent >= 70)
            {

            }
            else 
            {
                progressTracker.UpdateProgress(percent); // เพิ่มค่าโดยไม่ต้องทำลาย cube
            }
        }
    }
}
