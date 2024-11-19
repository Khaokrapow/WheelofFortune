using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BlockProgress : MonoBehaviour
{
    public float percent = 10f; // ��˹������繵�������� Inspector
    private ProgressTracker progressTracker;

    void Start()
    {
        progressTracker = FindObjectOfType<ProgressTracker>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ��ͧ�ѹ�����͹��Ѻ�ҧ��ǹ
            if (progressTracker.currentProgress <= 10 && percent >= 70)
            {

            }
            else 
            {
                progressTracker.UpdateProgress(percent); // �������������ͧ����� cube
            }
        }
    }
}
