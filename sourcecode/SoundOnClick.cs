using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnClick : MonoBehaviour
{
    public AudioClip clickSound; // ��Ի���§�������
    public AudioSource audioSource; // ��� AudioSource ������������§

    
    // �ѧ��ѹ���ж١���¡������ա�ä�ԡ
    public void PlaySound()
    {
        if (clickSound != null && audioSource != null)
        {
            // ��駤�� AudioClip ����Ѻ AudioSource
            audioSource.clip = clickSound;

            // ������§
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource ���� AudioClip �ѧ������駤��!");
        }
    }
}
